using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public class CategoryRepositoryInMemory : ICategoryRepository
    {
        static List<CategoryModel> categories = new List<CategoryModel>
        {
            new CategoryModel{CategoryId = 1, CategoryName = "Tivi", CategoryDescription= "abc"},
            new CategoryModel{CategoryId = 2, CategoryName = "Tủ lạnh", CategoryDescription= "cde"},
            new CategoryModel{CategoryId = 3, CategoryName = "Điều hòa", CategoryDescription= "efh"},
            new CategoryModel{CategoryId = 4, CategoryName = "Máy giặt", CategoryDescription= "xml"},
        };


        public CategoryModel Add(CategoryVM category)
        {
            var _category = new CategoryModel
            {
                CategoryId = categories.Max(cate => cate.CategoryId) + 1,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
            };
            categories.Add(_category);
            return _category;

        }

        public void Delete(int id)
        {
            var _category = categories.SingleOrDefault(lo => lo.CategoryId == id);
            categories.Remove(_category);
        }

        public List<CategoryModel> GetAll()
        {
            return categories;

        }

        public CategoryModel GetCategoryById(int id)
        {

            return categories.SingleOrDefault(lo => lo.CategoryId == id);
        }

        public void Update(int id, CategoryVM category)
        {
            var _category = categories.SingleOrDefault(lo => lo.CategoryId == id);
            if (_category != null)
            {
                _category.CategoryName = category.CategoryName;
            }
        }
    }
}
