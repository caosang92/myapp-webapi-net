using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Models;

namespace MyWebApiApp.Data
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region Create DBSet 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(ord => ord.OrderId);
                e.Property(ord => ord.OrderDate).HasDefaultValue(DateTime.Now);
            });
        }
    }
}
