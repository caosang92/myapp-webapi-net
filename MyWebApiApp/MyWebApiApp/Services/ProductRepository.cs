using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System.Linq;

namespace MyWebApiApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private MyDBContext _context;
        public static int PAGE_SIZE { get; set; } = 5;
        public ProductRepository(MyDBContext context) {
            _context = context;
        }
        public ProductModel Add(ProductVM product)
        {
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Discount = product.Discount,
                CategoryID = product.CategoryID,
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            return new ProductModel{
                ProductId = newProduct.ProductId,
                ProductName = newProduct.ProductName,
                ProductDescription = newProduct.ProductDescription,
                Price = newProduct.Price,
                Discount = newProduct.Discount,
                CategoryID = newProduct.CategoryID,
                //CategoryName = newProduct.Category.CategoryName
            };
        }

        public void Delete(int id)
        {
            var prod_result = _context.Products.SingleOrDefault(prod => prod.ProductId == id);
            if (prod_result != null)
            {
                _context.Remove(prod_result);
                _context.SaveChanges();
            };
        }

        public List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.Products.Include(prod => prod.Category).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(pro => pro.ProductName.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(pro => pro.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(pro => pro.Price <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name(Product Name)
            allProducts = allProducts.OrderBy(pro => pro.ProductId);

            if (sortBy != null)
            {
                switch(sortBy)
                {
                    case "ProdName_desc": allProducts = allProducts.OrderByDescending(pro => pro.ProductName); break;
                    case "Price-desc": allProducts = allProducts.OrderByDescending(pro => pro.Price); break;
                    case "Price-asc": allProducts = allProducts.OrderBy(pro => pro.Price); break;
                }
            }
            #endregion

            #region Pagination
            //allProducts= allProducts.Skip((page-1)* PAGE_SIZE).Take(PAGE_SIZE);
           

            //var result = allProducts.Select(pro => new ProductModel
            //    {
            //    ProductId = pro.ProductId,
            //    ProductName = pro.ProductName,
            //    ProductDescription=pro.ProductDescription,
            //    Discount = pro.Discount,
            //    Price = pro.Price,
            //    CategoryID = pro.CategoryID,
            //    CategoryName = pro.Category.CategoryName,
            //});
            //return result.ToList();

            var result = PaginatedList<Product>.Create(allProducts, page, PAGE_SIZE);
            return result.Select(pro => new ProductModel
            {
                ProductId = pro.ProductId,
                ProductName = pro.ProductName,
                ProductDescription = pro.ProductDescription,
                Discount = pro.Discount,
                Price = pro.Price,
                CategoryID = pro.CategoryID,
                CategoryName = pro.Category.CategoryName,
            }).ToList();
            
            #endregion

        }


        public ProductModel? GetProductById(int id)
        {
            var prod_result = _context.Products.SingleOrDefault(prod => prod.ProductId == id);
            if (prod_result != null)
            {
                return new ProductModel
                {
                    ProductId = prod_result.ProductId,
                    ProductName = prod_result.ProductName,
                    ProductDescription = prod_result.ProductDescription,
                    Discount = prod_result.Discount,
                    Price = prod_result.Price,
                    CategoryID = prod_result.CategoryID,
                    CategoryName = String.Empty,//prod_result.Category.CategoryName,
                };
            }
            return null;
        }

        public void Update(int id,ProductVM product)
        {
            var prod_result = _context.Products.SingleOrDefault(prod => prod.ProductId == id);

            if (prod_result != null)
            {
                prod_result.ProductName = product.ProductName;
                prod_result.ProductDescription = product.ProductDescription;
                prod_result.Price = product.Price;
                prod_result.CategoryID = product.CategoryID;
                prod_result.Discount = product.Discount;
                /*prod_result. = product.ProductName;*/
                _context.SaveChanges();
            }
            else { return; }
        }
    }
}
