using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Grupp3Hattmakaren.Models
{
    public class HatContext : DbContext
    {
        public HatContext(DbContextOptions<HatContext> options) : base(options) 
        { 
        }

        
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
