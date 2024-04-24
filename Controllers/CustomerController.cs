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
        private readonly SignInManager<User> _signInManager;

        public CustomerController(HatContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CustomerOrderForm()
        {
            List<Material> materials = _context.Materials.ToList();
            ViewBag.Materials = materials;
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
                isInProgress = enquiryViewModel.isInProgress,
                getInStore = true


            };
            _context.Enquiries.Add(newEnquiry);
            _context.SaveChanges();

            var customer = await _userManager.FindByNameAsync(User.Identity.Name);
            var shoppingCartItems = _context.ProductShoppingCarts
                .Where(item => item.shoppingCart.customerId == customer.Id)
                .ToList();

            _context.ProductShoppingCarts.RemoveRange(shoppingCartItems);
            _context.SaveChanges();

            return View("EnquiryConfirmationMessage", enquiryViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteAccount(string password)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Verifiera lösenordet
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                ModelState.AddModelError("", "Password is incorrect");
                return View("CustomerDelete");
            }

            // Kolla om det finns pågående ordrar
            //var hasOngoingOrders = _context.Orders.Any(o => o.CustomerId == user.Id && o.isPayed == false);
            //if (hasOngoingOrders)
            //{
            //    ModelState.AddModelError("", "Cannot delete account with ongoing orders.");
            //    return View("CustomerDelete");
            //}

            // Radera kundens relaterade data
            var addresses = _context.Addresses.Where(a => a.CustomerId == user.Id);
            _context.Addresses.RemoveRange(addresses);

            var orders = _context.Orders.Where(o => o.CustomerId == user.Id);
            _context.Orders.RemoveRange(orders);

            await _context.SaveChangesAsync();

            // Till sist raderas kunden
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete the account.");
                return View("CustomerDelete");
            }
        }


        [Authorize(Roles = "Customer")]
            public IActionResult CustomerMessages()
            {
                return View();
            }

        [Authorize(Roles = "Customer")]
        public IActionResult CustomerMyOrders()
        {
            {
                var userId = _userManager.GetUserId(User);
                List<Enquiry> enquirys = new List<Enquiry>();
                foreach (var enquiry in _context.Enquiries.ToList())
                {
                    if (enquiry.CustomerId == userId)
                    {
                        enquirys.Add(enquiry);
                    }
                }
                ViewBag.enquiryList = _context.Enquiries.ToList();
                return View();
            }
        }


        [Authorize(Roles = "Customer")]
            public IActionResult CustomerOrderHistory()
            {
                var userId = _userManager.GetUserId(User);
                 List<Order> orders = new List<Order>();
                foreach (var order in _context.Orders.ToList())
            {
                if (order.CustomerId == userId)
                {
                    orders.Add(order);
                }
            }
                ViewBag.orderList = _context.Orders.ToList();
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

            [Authorize(Roles = "Customer")]
            public IActionResult CustomerDelete()
            {
                return View();
            }

    }

    }
  


