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
                isInProgress = enquiryViewModel.isInProgress,
                isSpecial = enquiryViewModel.isSpecial,
                fabricMaterial = enquiryViewModel.fabricMaterial,
                specialEffectMaterials = enquiryViewModel.specialEffectMaterials,
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
                return View();
            }

            [Authorize(Roles = "Customer")]
            public IActionResult CustomerOrderHistory()
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
  


