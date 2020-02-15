using CatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatApi.Data
{
    public class CatContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public CatContext(DbContextOptions<CatContext> options)
            : base(options)
        {
            
        }
    }
}
