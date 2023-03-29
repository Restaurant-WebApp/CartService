using CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Data
{
    public class CartAppDbContext: DbContext
    {
        public CartAppDbContext(DbContextOptions<CartAppDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
