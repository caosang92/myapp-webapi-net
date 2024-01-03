using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll(string search);
        ProductVM GetProductById(int id);
        ProductVM Add(ProductVM product);
        void Update(ProductVM product);
        void Delete(int id);
    }
}
