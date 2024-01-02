using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDBContext _context;

        public CategoriesController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listCategory = _context.Categories.ToList();
            return Ok(listCategory);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var listCategory = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);

            try
            {
                //LINQ Object Query
                if (listCategory == null)
                {
                    return NotFound();
                }
                return Ok(listCategory);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Create(CategoryModel cateModel)
        {
            try
            {
                var cate = new Category
                {
                    CategoryName = cateModel.CategoryName,
                    CategoryDescription = cateModel.CategoryDescription
                };
                _context.Add(cate);
                _context.SaveChanges();
                return Ok(cate);
            }
           catch
            {  return BadRequest(); }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCateById(int id, CategoryModel cateModel)
        {
            var cate = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);

            try
            {
                //LINQ Object Query
                if (cate != null)
                {
                    cate.CategoryName = cateModel.CategoryName;
                    cate.CategoryDescription = cateModel.CategoryDescription;
                    _context.SaveChanges();
                    return NoContent();
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
