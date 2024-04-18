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
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product_Material> Product_Materials { get; set; }
        public DbSet<ProductShoppingCart> ProductShoppingCarts { get; set; }
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

            modelBuilder.Entity<ProductShoppingCart>()
                .HasKey(psc => new { psc.productId, psc.shoppingCartId });
            //modelBuilder.Entity<Customer>()
            //    .HasOne(c => c.cart)
            //    .WithOne(sc => sc.customer);

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

            modelBuilder.Entity<Material>().HasData(
              
                // Stråhattar
                new Material
                {
                    materialId = 1,
                    name = "Rishalm",
                    quantity = 1000,
                    supplier = "Temu",
                    price = 599
                },
                new Material
                {
                    materialId = 2,
                    name = "Palmlöv",
                    quantity = 800,
                    supplier = "Temu",
                    price = 749
                },
                new Material
                {
                    materialId = 3,
                    name = "Majsblad",
                    quantity = 1200,
                    supplier = "Wish",
                    price = 625
                },
                new Material
                {
                    materialId = 4,
                    name = "Hampfibrer",
                    quantity = 1500,
                    supplier = "Shein",
                    price = 8.99
                },
                // Tyghattar
                new Material
                {
                    materialId = 5,
                    name = "Bomull",
                    quantity = 2000,
                    supplier = "ICA Maxi Bogulundsängen",
                    price = 499
                },
                new Material
                {
                    materialId = 6,
                    name = "Linne",
                    quantity = 1800,
                    supplier = "Ikea",
                    price = 649
                },
                new Material
                {
                    materialId = 7,
                    name = "Ull",
                    quantity = 1600,
                    supplier = "Får I Närke AB",
                    price = 999
                },
                // Läderhättor
                new Material
                {
                    materialId = 8,
                    name = "Läder",
                    quantity = 2200,
                    supplier = "Läder Byxan AB",
                    price = 1299
                },
                // Fjädrar
                new Material
                {
                    materialId = 9,
                    name = "Fjädrar från strutsar",
                    quantity = 300,
                    supplier = "Lannas Strutsfarm",
                    price = 399
                },
                new Material
                {
                    materialId = 10,
                    name = "Fjädrar från påfåglar",
                    quantity = 250,
                    supplier = "Lannas Strutsfarm",
                    price = 549
                },
                new Material
                {
                    materialId = 11,
                    name = "Fjädrar från höns",
                    quantity = 400,
                    supplier = "Kronfågeln",
                    price = 299
                },
                // Tygblommor
                new Material
                {
                    materialId = 12,
                    name = "Tygblommor",
                    quantity = 500,
                    supplier = "Majblomman",
                    price = 799
                },
                // Pärlor
                new Material
                {
                    materialId = 13,
                    name = "Pärlor",
                    quantity = 600,
                    supplier = "Förskolan Myrorna",
                    price = 699
                },
                // Spets
                new Material
                {
                    materialId = 14,
                    name = "Spets",
                    quantity = 700,
                    supplier = "Victoria Secret",
                    price = 849
                },
                // Lackerat papper
                new Material
                {
                    materialId = 15,
                    name = "Lackerat papper",
                    quantity = 450,
                    supplier = "Dunder Mifflin",
                    price = 599
                },
                // Lurextråd
                new Material
                {
                    materialId = 16,
                    name = "Lurextråd",
                    quantity = 550,
                    supplier = "Närke Slakteri AB",
                    price = 449
                },
                // Fuskpäls
                new Material
                {
                    materialId = 17,
                    name = "Fuskpäls",
                    quantity = 350,
                    supplier = "MotherLoad AB",
                    price = 1099
                },
                 new Material
                 {
                     materialId = 18,
                     name = "Kaninfilt",
                     quantity = 10,
                     supplier = "Kaninens KooperationAB",
                     price = 105
                 },
                 new Material
              {
                  materialId = 19,
                  name = "Ull",
                  quantity = 200,
                  supplier = "Kaninens KooperationAB",
                  price = 200
              },
              new Material
              {
                  materialId = 20,
                  name = "Toquillastrå",
                  quantity = 300,
                  supplier = "Ecuadour Finest AB",
                  price = 3075
              }
                
                );

            


        }
    
    }
}

