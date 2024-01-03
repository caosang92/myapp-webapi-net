using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetAll();
        CategoryModel GetCategoryById(int id);
        CategoryModel Add(CategoryVM category);
        void Update(CategoryModel cateModel);
        void Delete(int id);

    }
}
