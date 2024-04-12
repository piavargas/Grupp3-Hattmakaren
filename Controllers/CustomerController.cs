using Grupp3Hattmakaren.Models;
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
        public IActionResult CustomerOrderForm(Enquiry enquiry)
        {

            var newEnquiry = new Enquiry
            {
                consentHat = enquiry.consentHat,
                description = enquiry.description,
                font = enquiry.font,
                textOnHat = enquiry.textOnHat,

            };

            // Lägg till Enquiry-objektet i context och spara ändringar i databasen
            _context.Enquiries.Add(newEnquiry);
            _context.SaveChanges();

            // Returnera en vy med det nya enquiry-objektet
            return View(enquiry);
        }


    }
}

