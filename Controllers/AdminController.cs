using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grupp3Hattmakaren.Controllers
{
    public class AdminController : Controller
    {
        private readonly HatContext _hatcontext;
        private readonly UserManager<User> _userManager;

        public AdminController(HatContext context, UserManager<User> userManager)
        {
            _hatcontext = context;
            _userManager = userManager;

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminEnquiries()
        {
            var enquiries = _hatcontext.Enquiries.ToList();
            return View(enquiries);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminOrders()
        {
            var enquiries = _hatcontext.Enquiries.Include(e => e.Customer).ToList();
            List<Order> orderList = _hatcontext.Orders.ToList();
            //foreach(Order order in orderList) 
            //{ 
            //    if(order.isPayed || !order.Enquiry.isInProgress) 
            //    { 
            //        orderList.Remove(order);
            //    }
            //}
            ViewBag.orderList = orderList;
            return View(enquiries);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeStatus(int enquiryId)
        {
            // Hämta Enquiry-objektet från databasen baserat på enquiryId
            var enquiry = _hatcontext.Enquiries.Find(enquiryId);

            // Om inget Enquiry-objekt hittades, returnera NotFound
            if (enquiry == null)
            {
                return NotFound();
            }

            enquiry.isInProgress = true;

            // Spara ändringarna i databasen
            _hatcontext.SaveChanges();


            var address = _hatcontext.Addresses.FirstOrDefault(a => a.CustomerId == enquiry.CustomerId);

            // Skapa en ny Order baserad på informationen från Enquiry-objektet och den nya produkten
            var order = new Order
            {
                price = 5,
                isPayed = false,
                CustomerId = enquiry.CustomerId,
                AddressId = address.AddressId,
                //ProductId = product.ProductId, // Använd den nya produkten
                EnquiryId = enquiryId
            };

            // Lägg till den nya ordern i databasen
            _hatcontext.Orders.Add(order);
            _hatcontext.SaveChanges();



            return RedirectToAction("AdminEnquiries");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangePaymentStatus(int orderId)
        {
            var order = _hatcontext.Orders.Find(orderId);

            if (order == null)
            {
                return NotFound();
            }

            // Uppdatera status till "paid"
            order.isPayed = true;

            _hatcontext.SaveChanges();

            // Återgå till AdminOrders-åtgärden efter att ändringen har gjorts
            return RedirectToAction("AdminOrders");
        }

        //[HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOrderHistory()
        {
            // Hämta en lista över alla betalda ordrar
            var paidOrders = _hatcontext.Orders
                .Where(order => order.isPayed)
                .Include(order => order.Customer)
                .ToList();

            // Skicka den filtrerade listan till vyn
            ViewBag.oldOrderList = paidOrders;
            return View();


        }
    }
}

