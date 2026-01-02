using BlazorServerCRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerCRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
