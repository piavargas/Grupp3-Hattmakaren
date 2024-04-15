using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using static System.Net.Mime.MediaTypeNames;

namespace Grupp3Hattmakaren.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HatContext _context;

        public CustomerController(HatContext context)
        {

            _context = context;
        }

        public IActionResult CustomerOrderForm()
        {
            return View(new EnquiryViewModel());
        }


        [HttpPost]
        public IActionResult CustomerOrderForm(EnquiryViewModel enquiryViewModel)
        {

            var enquiry = new Enquiry
            {
                consentHat = enquiryViewModel.consentHat,
                description = enquiryViewModel.description,
                font = enquiryViewModel.font,
                textOnHat = enquiryViewModel.textOnHat,

            };

            // Lägg till Enquiry-objektet i context och spara ändringar i databasen
            _context.Enquiries.Add(enquiry);
            _context.SaveChanges();

            // Returnera en vy med det nya enquiry-objektet
            return View(enquiry);
        }


        [Authorize(Roles = "Customer")]
        public IActionResult CustomerMessages()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerMyOrders()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerOrderHistory()
        {
            return View();
        }

    }


  
}

