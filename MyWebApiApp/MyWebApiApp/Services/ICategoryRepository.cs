using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetAll();
        CategoryModel GetCategoryById(int id);
        CategoryModel Add(CategoryVM category);
        void Update(int id, CategoryVM cateModel);
        void Delete(int id);

    }
}
