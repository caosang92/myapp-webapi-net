using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly AppSetting _appSetting;
        public UserController(MyDBContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue; 
        }
        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.Users.SingleOrDefault(p => p.UserName == model.UserName && model.Password == p.Password);
            if (user == null)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "Invalid usernam/password",

                });
            }

            //cấp token
            return Ok(new APIResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserName", user.UserName),
                    new Claim("UserId", user.UserId.ToString()),


                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString() )
                } ),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);

        }
    }
}
