using KjssAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace KjssAPI.Data
{
    public class ProductDBContext:DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
