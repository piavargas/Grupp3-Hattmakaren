using Grupp3Hattmakaren.Models;

namespace Grupp3Hattmakaren.Controllers
{
    public class OrderService
    {
        private readonly HatContext _context;

        public OrderService(HatContext context)
        {
            _context = context;
        }

        public OrderSummaryViewModel GetOrderSummary(int orderId)
        {
            var order = _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderSummaryViewModel
                {
                    OrderId = o.OrderId,
                    name = o.Customer.firstName + " " + o.Customer.lastName,
                    //Phone = o.Customer.PhoneNumber,  // Hur hämtar man nummer från IdentityUser?
                    email = o.Customer.email,
                    products = o.products.Select(p => new ProductSummary
                    {
                        productName = p.productName,
                        description = p.description,
                        materials = p.materials.Select(m => m.name).ToList(),  // Assuming there is a 'Name' property in Material
                        size = p.size,
                        price = p.price
                    }).ToList(),
                    TotalPrice = o.products.Sum(p => p.price)
                }).SingleOrDefault();

            return order;
        }
    }

}
