using Microsoft.EntityFrameworkCore;
using MyProj.WebApi.Entities.Products;

namespace MyProj.WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
        { }
    }
}
