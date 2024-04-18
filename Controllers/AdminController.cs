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
            ViewBag.Enquiries = enquiries != null ? enquiries : new List<Enquiry>();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeStatus(int enquiryId)
        {
            try
            {
                var enquiry = _hatcontext.Enquiries
                    .Include(e => e.Customer)
                    .FirstOrDefault(e => e.EnquiryId == enquiryId);

                if (enquiry != null)
                {
                    enquiry.isInProgress = true; // Uppdatera status för förfrågan till "in progress"
                    _hatcontext.SaveChanges();

                    // Skapa en order för den aktuella förfrågan
                    var order = new Order
                    {
                        CustomerId = enquiry.CustomerId,
                        // Fyll i andra relevanta egenskaper för ordern, t.ex. pris, betalstatus etc.
                    };

                    _hatcontext.Orders.Add(order);
                    _hatcontext.SaveChanges();

                    // Returnera den uppdaterade förfrågan till klienten
                    return Json(new { success = true, enquiry = enquiry });
                }

                return Json(new { success = false, error = "Enquiry not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


    }

}

