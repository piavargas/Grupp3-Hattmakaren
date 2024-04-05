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
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product_Material> Product_Materials { get; set; }
        public DbSet<ShippingBill> ShippingBills { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SpecialProduct> SpecialProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product_Material>()
                .HasKey(pm => new { pm.ProductId, pm.MaterialId });

            modelBuilder.Entity<Order>()
              .HasOne(o => o.Customer)
              .WithMany(c => c.orders)
              .HasForeignKey(o => o.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.products)
                .WithMany(p => p.orders)
                .UsingEntity(j => j.ToTable("OrderProduct"));



            //modelBuilder.Entity<Enquiry>()
            //    .HasOne()




            //modelBuilder.Entity<Address>()
            //    .HasOne(a => a.Customer)
            //    .WithMany(cs => cs.addresses)
            //    .HasForeignKey(cs => cs.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict);






        }



    }
}
