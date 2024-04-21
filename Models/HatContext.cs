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
                    productName = "Classic Bowler Hat",
                    description = "A timeless bowler hat made from high-quality wool felt, perfect for both formal and casual occasions.",
					ImagePath = "/NewFolder/produkthatt.jpg",
					size = 58,
                },
                new Product
                {
                    ProductId = 2,
                    productName = "Elegant Ladies' Hat",
                    description = "An elegant hat with a wide brim and a decorative silk ribbon. Ideal for sunny days or a stylish outing.",
					ImagePath = "/NewFolder/produkthatt2.png",
					size = 56,
                },
                new Product
                {
                    ProductId = 3,
                    productName = "Adventurer's Fedora",
                    description = "Sturdy and ready for any adventure, this fedora is your faithful companion in all weathers.",
					ImagePath = "/NewFolder/produkthatt3.png",
					size = 59,
                },
                new Product
                {
                    ProductId = 4,
                    productName = "Vintage Top Hat",
                    description = "Relive the roaring 1920s with this authentic top hat, perfect for themed parties and gatherings.",
					ImagePath = "/NewFolder/produkthatt4.jpg",
					size = 60,
                },
                new Product
                {
                    ProductId = 5,
                    productName = "Modern Beret",
                    description = "A chic beret made of 100% wool, available in various colors. Add a French touch to your wardrobe.",
					ImagePath = "/NewFolder/produkthatt5.webp",
					size = 57,
                },
                new Product
                {
                    ProductId = 6,
                    productName = "Panama-style Sun Hat",
                    description = "Light and airy, this Panama hat provides sun protection while keeping you cool and stylish.",
					ImagePath = "/NewFolder/produkthatt7.jpg",
					size = 58,
                }
            );


            modelBuilder.Entity<Enquiry>().HasData(
               new Enquiry
               {
                   EnquiryId = 1,                 
                   consentHat = true,
                   description = "Jag är intresserad av att beställa en hatt med speciellt tryck.",
                   font = "Arial",
                   textOnHat = "Jonas är bäst",
                   isInProgress = true,                  
                   CustomerId = "1"
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
