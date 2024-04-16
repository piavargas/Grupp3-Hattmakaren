using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Grupp3Hattmakaren.Models
{
    public class HatContext : IdentityDbContext<User>
    {

        public HatContext(DbContextOptions<HatContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
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

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);


            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   ProductId = 1,
                   productName = "Magisk Nalle Natlampa",
                   description = "Denna mysiga nalle tänds när du rör honom.",
                   size = 350,
               }

               ); 
        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  


            modelBuilder.Entity<Order>().HasData(
               new Order
               {
                   OrderId = 1,
                   price = 150.00,
                   CustomerId = "1",
                   isPayed = true,
                   AddressId = 1,
                   ProductId = 1
               }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    AddressId = 1,
                    CustomerId = "1", // Länka detta till en befintlig Customer                   
                    streetName = "123 Main Street",
                    zipCode = 12345,
                    countryName = "Countryland"
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = "1",
                    UserName = "jonasmoll",
                    Email = "jonasmoll@outlook.com",
                    firstName = "Jonas",
                    lastName = "Moll",
                    headSize = "28cm"
                }
            );

        
        }
    
    }
}

