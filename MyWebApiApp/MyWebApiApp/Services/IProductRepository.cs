using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1);
        ProductModel GetProductById(int id);
        ProductModel Add(ProductVM product);
        void Update(int id, ProductVM product);
        void Delete(int id);
    }
}
