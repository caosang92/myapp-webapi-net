using MyWebApiApp.Data;
using MyWebApiApp.Models;

namespace MyWebApiApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private MyDBContext _context;

        public ProductRepository(MyDBContext context) {
            _context = context;
        }
        public ProductVM Add(ProductVM product)
        {
            var NewProduct = new ProductVM()
            {
                ProductName = product.ProductName,
                Price = product.Price,
            };
            _context.Add(NewProduct);
            _context.SaveChanges();
            return new ProductVM
            {
                ProductName = product.ProductName,
                Price = product.Price
            };
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetAll(string search)
        {
            var allProducts = _context.Products.Where(pro=> pro.ProductName.Contains(search));
            var result = allProducts.Select(pro => new ProductModel {
                ProductId = pro.ProductId,
                ProductName = pro.ProductName,
                Price = pro.Price,
                CategoryName = pro.Category.CategoryName,

            });
            return result.ToList();
        }


        public ProductVM GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductVM product)
        {
            throw new NotImplementedException();
        }
    }
}
