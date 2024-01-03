using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var listCategory = _context.Categories.ToList();
                return Ok(listCategory);
            }
            catch
            {
                return BadRequest();
            }

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
        public IActionResult Create(CategoryVM cateVM)
        {
            try
            {
                var cate = new Category
                {
                    CategoryName = cateVM.CategoryName,
                    CategoryDescription = cateVM.CategoryDescription
                };
                _context.Add(cate);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, cate);
            }
            catch
            { return BadRequest(); }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCateById(int id, CategoryVM cateModel)
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

        [HttpDelete("{id}")]
        public IActionResult DeleteCateById(int id)
        {
            var cate = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);

            try
            {
                if (cate != null)
                {
                    _context.Categories.Remove(cate);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK);
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
