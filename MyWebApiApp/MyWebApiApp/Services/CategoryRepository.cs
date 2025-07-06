using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWebApiApp.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDBContext _context;

        public CategoryRepository(MyDBContext context)
        {
            _context = context;
        }
        
        public CategoryModel Add(CategoryVM category)
        {
            var cate = new Category
            {
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };
            _context.Add(cate);
            _context.SaveChanges();
            return new CategoryModel
            {
                CategoryId = cate.CategoryId,
                CategoryName = cate.CategoryName,
                CategoryDescription = cate.CategoryDescription
            };
        }

        public void Delete(int id)
        {
            var cate_result = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);
            if (cate_result != null)
            {
                _context.Remove(cate_result);
                _context.SaveChanges();
            };
        }


        public List<CategoryModel> GetAll()
        {
            var categories = _context.Categories.Select(cate => new CategoryModel
            {
                CategoryId = cate.CategoryId,
                CategoryDescription = cate.CategoryDescription,
                CategoryName = cate.CategoryName,
            });
            return categories.ToList();
        }

        public CategoryModel GetCategoryById(int id)
        {
            var cate_result = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);
            if (cate_result != null)
            {
                return new CategoryModel
                {
                    CategoryId = cate_result.CategoryId,
                    CategoryDescription = cate_result.CategoryDescription,
                    CategoryName = cate_result.CategoryName
                };
            }
            return null;
        }

        public void Update(int id, CategoryVM category)
        {
            var cate_result = _context.Categories.SingleOrDefault(cate => cate.CategoryId == id);

            if (cate_result != null)
            {
                 cate_result.CategoryDescription = category.CategoryDescription;
                 cate_result.CategoryName = category.CategoryName;
                _context.SaveChanges();
            }
            else
            {
                return;
            }
        }
    }
}
