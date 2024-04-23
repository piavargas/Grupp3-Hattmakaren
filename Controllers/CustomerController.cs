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

        [HttpGet]
        public IActionResult CustomerOrderForm()
        {
            return View(new EnquiryViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> CustomerOrderForm(EnquiryViewModel enquiryViewModel)
        {
            // Validera endast de specifika fälten
            ModelState.Remove("specialEffectMaterials");
            ModelState.Remove("textOnHat");
            ModelState.Remove("font");
            ModelState.Remove("description");
            ModelState.Remove("consentHat");
            ModelState.Remove("firstName");
            ModelState.Remove("lastName");
            ModelState.Remove("email");
            ModelState.Remove("streetName");
            ModelState.Remove("zipCode");
            ModelState.Remove("countryName");
            ModelState.Remove("isInProgress");
            ModelState.Remove("isSpecial");
            ModelState.Remove("getInStore");

            if (ModelState.IsValid)
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
            else
            {
                return View(enquiryViewModel);
            }
        }


        public IActionResult CardPaymentMethod()
        {
            return View();
        }

        public IActionResult PaymentConfirmationMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DefaultCustomerOrderForm(EnquiryViewModel enquiryViewModel)
        {
            var newEnquiry = new Enquiry
            {
                CustomerId = _userManager.GetUserId(User),
                isInProgress = enquiryViewModel.isInProgress

            };

            _context.Enquiries.Add(newEnquiry);
            _context.SaveChanges();

            return View("EnquiryConfirmationMessage", enquiryViewModel);


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
        [HttpPost]
        public IActionResult RemoveProductFromCart(int productId)
        {
            // Hitta kundvagnen för den aktuella användaren (exempelvis genom att använda användarens identitet)
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var customerCart = _context.ShoppingCarts.FirstOrDefault(cart => cart.customerId == user.Id);

            if (customerCart != null)
            {
                var productSC = _context.ProductShoppingCarts.FirstOrDefault(pSC => pSC.shoppingCartId == customerCart.Id && pSC.productId == productId);
                // Hitta den specifika produkten i kundvagnen
                if(productSC != null)
                {
                    _context.ProductShoppingCarts.Remove(productSC);
                    _context.SaveChanges();
                }
            }

            // Redirect tillbaka till sammanställningssidan
            return RedirectToAction("SummaryCart", "Customer");
        }

        [Authorize(Roles = "Customer")]
        public IActionResult SummaryCart()
        {
            return View();

        }

    }

    }
  


