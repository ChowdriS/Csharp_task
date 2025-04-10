using Microsoft.EntityFrameworkCore;
using Task10.Model;

namespace Task10.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Product> Product { get; set; }
    }
}
