using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using static System.Net.Mime.MediaTypeNames;

namespace Grupp3Hattmakaren.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HatContext _context;
        private readonly UserManager<User> _userManager;
        
        public CustomerController(HatContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public IActionResult CustomerOrderForm()
        {
            return View(new EnquiryViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> CustomerOrderForm(EnquiryViewModel enquiryViewModel)
        {


            var newEnquiry = new Enquiry
            {
                consentHat = enquiryViewModel.consentHat,
                description = enquiryViewModel.description,
                font = enquiryViewModel.font,
                textOnHat = enquiryViewModel.textOnHat,
                CustomerId = _userManager.GetUserId(User)
            };

                _context.Enquiries.Add(newEnquiry);
                _context.SaveChanges();

            var newAddress = new Address
            {
                streetName = enquiryViewModel.streetName,
                zipCode = enquiryViewModel.zipCode,
                countryName = enquiryViewModel.countryName,
                CustomerId = _userManager.GetUserId(User)
            };

              _context.Addresses.Add(newAddress);
              _context.SaveChanges();


            
            KundOrderFörfråga
            return View(enquiryViewModel);
        }


    }


  
}

