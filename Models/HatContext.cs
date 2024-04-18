using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class HatContext : IdentityDbContext<Admin>
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

            //Exempeldata ShippingBill
            modelBuilder.Entity<ShippingBill>().HasData(
                new ShippingBill
                {
                    ShippingBillId = 1,
                    productCode = "SHB001",
                    //OrderId = 1
                },
                new ShippingBill
                {
                    ShippingBillId = 2,
                    productCode = "SHB002",
                    //OrderId = 2
                },
                new ShippingBill
                {
                    ShippingBillId = 3,
                    productCode = "SHB003",
                    //OrderId = 3
                });

            //Exempeldata OrderSummary
            //modelBuilder.Entity<Order>().HasData(
            //   new Order
            //   {
            //       OrderId = 1,
            //       price = ,
            //       CustomerId = 1,
            //       Customer = ,
            //       AddressId = ,
            //       Address = ,
            //       ProductId = ,
            //       products = ,
            //   };




        }



    }
}
