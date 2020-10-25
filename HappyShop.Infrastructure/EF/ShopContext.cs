using HappyShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HappyShop.Infrastructure.EF
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=DESKTOP-4ULT7VC\\SQLEXPRESS;Initial Catalog=HappyShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    }
}
