using System.Data.Entity;

namespace aspnet2.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
    }
}
