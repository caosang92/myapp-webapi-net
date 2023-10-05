using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll() { 
        return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                //LINQ Object Query
                var product = products.SingleOrDefault(pd => pd.ProductID == Guid.Parse(id));
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
            var product = new Product { 
            ProductID = Guid.NewGuid(),
            ProductName = productVM.ProductName,
            Price = productVM.Price
            };
            products.Add(product);
            return Ok(new { 
            Success= true, Data = product,
            });
        }

        [HttpPut("id")]
        public IActionResult Update(string id, Product editproduct) {
            try
            {
                //LINQ Object Query
                var product = products.SingleOrDefault(pd => pd.ProductID == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                if(id != product.ProductID.ToString())
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
