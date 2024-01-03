using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public static List<ProductModel> products = new List<ProductModel>();

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public IActionResult GetAll(string search) {
            var result = _productRepository.GetAll(search).ToList();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                //LINQ Object Query
                var product = products.SingleOrDefault(pd => pd.ProductId == int.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            try
            {
                return Ok(_productRepository.Add(productVM));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("id")]
        public IActionResult Update(string id, ProductModel editproduct) {
            try
            {
                //LINQ Object Query
                var product = products.SingleOrDefault(pd => pd.ProductId == int.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                if(id != product.ProductId.ToString())
                {
                    return NotFound(product);
                }
                // thực hiện update
                product.ProductName = editproduct.ProductName;
                product.Price = editproduct.Price;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
