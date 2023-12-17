using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Models;

namespace MyWebApiApp.Data
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region Create DBSet 
        public DbSet<Product> Products { get; set; }
        #endregion
    }
}
