using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;

namespace RetroFootballWeb.Repository
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(c => new { c.CustomerId, c.ProductId, c.Size });
            modelBuilder.Entity<WishList>().HasKey(w => new { w.CustomerId, w.ProductId });
            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderID, o.ProductID, o.Size });
            modelBuilder.Entity<DeliveryInfo>().HasKey(d => new { d.CustomerID, d.Priority });
            base.OnModelCreating(modelBuilder);
        }
    }
}
