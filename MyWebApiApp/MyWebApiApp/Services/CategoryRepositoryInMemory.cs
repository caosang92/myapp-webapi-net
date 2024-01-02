using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public class CategoryRepositoryInMemory : ICategoryRepository
    {
        static List<CategoryVM> categories = new List<CategoryVM>
        {
            new CategoryVM{CategoryId = 1, CategoryName = "Tivi", CategoryDescription= "abc"},
            new CategoryVM{CategoryId = 2, CategoryName = "Tủ lạnh", CategoryDescription= "cde"},
            new CategoryVM{CategoryId = 3, CategoryName = "Điều hòa", CategoryDescription= "efh"},
            new CategoryVM{CategoryId = 4, CategoryName = "Máy giặt", CategoryDescription= "xml"},
        };


        public CategoryVM Add(CategoryModel category)
        {
            var _category = new CategoryVM
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

        public List<CategoryVM> GetAll()
        {
            return categories;

        }

        public CategoryVM GetCategoryById(int id)
        {

            return categories.SingleOrDefault(lo => lo.CategoryId == id);
        }

        public void Update(CategoryVM category)
        {
            var _category = categories.SingleOrDefault(lo => lo.CategoryId == category.CategoryId);
            if (_category != null)
            {
                _category.CategoryName = category.CategoryName;
            }


        }
    }
}
