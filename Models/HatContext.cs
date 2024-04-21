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


            modelBuilder.Entity<ProductShoppingCart>()
                .HasKey(psc => new { psc.productId, psc.shoppingCartId });
            //modelBuilder.Entity<Customer>()
            //    .HasOne(c => c.cart)
            //    .WithOne(sc => sc.customer);

            modelBuilder.Entity<Order>().HasData(
               new Order
               {
                   OrderId = 1,
                   price = 150.00,
                   CustomerId = "1",
                   isPayed = true,
                   AddressId = 1, 
                   ProductId = 1 
               },
                new Order
                {
                    OrderId = 2,
                    price = 130.00,
                    CustomerId = "2",
                    isPayed = true,
                    AddressId = 1,
                    ProductId = 1
                },
                 new Order
                 {
                     OrderId = 3,
                     price = 330.00,
                     CustomerId = "3",
                     isPayed = true,
                     AddressId = 2,
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
                },
                  new Address
                  {
                      AddressId = 2,
                      CustomerId = "2", // Länka detta till en befintlig Customer                   
                      streetName = "Potatisvägen",
                      zipCode = 70284,
                      countryName = "Sweden"
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
                },
                new Customer
                {
                    Id = "2",
                    UserName = "tanjahavstorm",
                    Email = "tanjahavstorm@outlook.com",
                    firstName = "Tanja",
                    lastName = "Havstorm",
                    headSize = "79cm"
                },
                new Customer
                {
                    Id = "3",
                    UserName = "maxmaxsson",
                    Email = "icamaxi@outlook.com",
                    firstName = "Max",
                    lastName = "Maxsson",
                    headSize = "21cm"
                }
            );

            modelBuilder.Entity<ShippingBill>().HasData(
    new ShippingBill
    {
        ShippingBillId = 1,
        productCode = "SHB001",
        OrderId = 1
    },
    new ShippingBill
    {
        ShippingBillId = 2,
        productCode = "SHB002",
        OrderId = 2
    },
    new ShippingBill
    {
        ShippingBillId = 3,
        productCode = "SHB003",
        OrderId = 3
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

