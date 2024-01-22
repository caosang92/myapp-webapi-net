using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Services;
using System.Globalization;
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
        public IActionResult GetAllProducts(string search, double? from, double? to, string sortBy, int page)
        {
            try
            {
                var result = _productRepository.GetAll(search, from, to, sortBy, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest("We can't get the product.");
            }
            
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var prod_result = _productRepository.GetProductById(id);
                if (prod_result == null)
                {
                    return Ok(prod_result); // NotFound();
                }
                return Ok(prod_result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize]
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
        public IActionResult Update(int id, ProductVM editproduct)
        {
            try
            {
                _productRepository.Update(id, editproduct);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProdById(int id)
        {
            try
            {
                _productRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}

