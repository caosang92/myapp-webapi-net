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
                e.Property(ord => ord.Receiver).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetail");
                e.HasKey(ord => new { ord.OrderId, ord.ProducId});
                e.HasOne(e => e.Order)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .HasConstraintName("FK_OrderDetail_Order");

                e.HasOne(e => e.Product)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.ProducId)
                .HasConstraintName("FK_OrderDetail_Product");

            });
        }
    }
}
