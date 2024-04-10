//using Grupp3Hattmakaren.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.EntityFrameworkCore;
//using Azure.Identity;
//using Microsoft.AspNetCore.Authorization;

//namespace Grupp3Hattmakaren.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly HatContext _hatcontext;
//        private UserManager<User> UserManager;
//        private SignInManager<User> signInManager;


//        public AccountController(HatContext context, UserManager<User> _userManager, SignInManager<User> _signInManager)
//        {
//            _hatcontext = context;
//            UserManager = _userManager;
//            signInManager = _signInManager;
//        }

//        public IActionResult LogIn()
//        {
//            LoginViewModel loginViewmodel = new LoginViewModel();
//            return View(loginViewmodel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await signInAdminManager.PasswordSignInAsync(
//                    loginViewModel.UserName,
//                    loginViewModel.PassWord,
//                    isPersistent: true,
//                    lockoutOnFailure: false
//                    );

//                if (result.Succeeded)
//                {
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Wrong username/password.");
//                }
//                return View(loginViewModel);
//            }
//            else
//            {

//                return RedirectToAction("Privacy", "Home");
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> LogOut()
//        {
//            await signInAdminManager.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }


//INNANN
//[HttpPost]
//public async Task <IActionResult> Register (RegisterViewModel registerViewModel)
//{
//    if (ModelState.IsValid)
//    {
//        var admin = new Admin()
//        {
//            UserName = registerViewModel.UserName,
//            firstName = registerViewModel.firstName,
//            lastName = registerViewModel.lastName,
//            employerCode = registerViewModel.employerCode
//        };

//        var result = await userManager.CreateAsync(admin, registerViewModel.PassWord);

//        if (result.Succeeded)
//        {
//            await signInManager.SignInAsync(admin, false);
//                return RedirectToAction("Index", "Home");
//        }

//        return View();
//    }
//    return View();
//}

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                IdentityResult result = null;

//                if (model.userRole == "Admin")
//                {
//                    var admin = new Admin { UserName = model.UserName, employerCode = model.employerCode };
//                    result = await AdminManager.CreateAsync(admin, model.PassWord);

//                    if (result.Succeeded)
//                    {
//                        await AdminManager.AddToRoleAsync(admin, "Admin");
//                        await signInAdminManager.SignInAsync(admin, isPersistent: false);
//                        return RedirectToAction("AdminDashboard", "Admin");
//                    }
//                }
//                else if (model.userRole == "Customer")
//                {
//                    var customer = new Customer { UserName = model.UserName };
//                    result = await CustomerManager.CreateAsync(customer, model.PassWord);

//                    if (result.Succeeded)
//                    {
//                        await CustomerManager.AddToRoleAsync(customer, "Customer");
//                        await _signInManager.SignInAsync(customer, isPersistent: false);
//                        return RedirectToAction("CustomerDashboard", "Customer");
//                    }
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid user role");
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError("", error.Description);
//                }
//            }

//            return View(model);
//        }
//    }
//}

using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Grupp3Hattmakaren.Controllers
{
    public class AccountController : Controller
    {
        private readonly HatContext _hatcontext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(HatContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _hatcontext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult LogIn()
        {
            LoginViewModel loginViewmodel = new LoginViewModel();
            return View(loginViewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginViewModel.UserName,
                    loginViewModel.PassWord,
                    isPersistent: true,
                    lockoutOnFailure: false
                    );

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username/password.");
                }
            }
            else
            {
                return RedirectToAction("Privacy", "Home");
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };

                var result = await _userManager.CreateAsync(user, model.PassWord);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.userRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (model.userRole == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else if (model.userRole == "Customer")
                    {
                        return RedirectToAction("CustomerDashboard", "Customer");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}

