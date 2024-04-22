using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
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
                headSize = enquiryViewModel.headSize,
                consentHat = enquiryViewModel.consentHat,
                description = enquiryViewModel.description,
                font = enquiryViewModel.font,
                textOnHat = enquiryViewModel.textOnHat,
                isInProgress = enquiryViewModel.isInProgress,
                isSpecial = enquiryViewModel.isSpecial,
                fabricMaterial = enquiryViewModel.fabricMaterial,
                specialEffectMaterials = enquiryViewModel.specialEffectMaterials,
                getInStore = enquiryViewModel.getInStore,
                color = enquiryViewModel.color,
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
            return View("EnquiryConfirmationMessage", enquiryViewModel);
                

        }

        public IActionResult CardPaymentMethod()
        {
            return View();
        }

        public IActionResult PaymentConfirmationMessage()
        {
            return View();
        }

            [Authorize(Roles = "Customer")]
            public IActionResult CustomerMessages()
            {
                return View();
            }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerMyOrders()
        {
            var userId = _userManager.GetUserId(User);

            var orders = _context.Orders
                .Where(o => o.CustomerId == userId)
                .ToList();

            return View(orders);
        }

        [Authorize(Roles = "Customer")]
            public IActionResult CustomerOrderHistory()
            {
                return View();
            }


        //public IActionResult acceptOffer ()
        //{

        //}


        }

    }
  


