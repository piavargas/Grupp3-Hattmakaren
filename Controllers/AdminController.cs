using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grupp3Hattmakaren.Controllers
{
    public class AdminController : Controller
    {
        private readonly HatContext _hatcontext;

        public AdminController (HatContext context)
        {
            _hatcontext = context;

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
            var orders = _hatcontext.Orders.Include(o => o.Customer).ToList();
            ViewBag.Orders = orders; 
            return View();
        }
    }
}
