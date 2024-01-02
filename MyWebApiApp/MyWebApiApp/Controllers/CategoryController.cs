using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using MyWebApiApp.Services;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_categoryRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var cate_result = _categoryRepository.GetCategoryById(id);
                if (cate_result == null)
                {
                    return NotFound();
                }else
                return Ok(cate_result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            try
            {
                return Ok(_categoryRepository.Add(category));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCateById(int id, CategoryVM cateVM)
        {
            if(id != cateVM.CategoryId)
            {
                return BadRequest();
            }
            try
            {
                _categoryRepository.Update(cateVM);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCateById(int id)
        {
            try {
                _categoryRepository.Delete(id);
                return Ok();
            }catch {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
