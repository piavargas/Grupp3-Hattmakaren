using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;

namespace Grupp3Hattmakaren.Controllers
{
    public class AccountController : Controller
    {
        private readonly HatContext _hatcontext;
        private UserManager<Admin> userManager;
        private SignInManager<Admin> signInManager;
        public AccountController(HatContext _context, UserManager <Admin> _user, SignInManager<Admin>_signIn)
        {
            _hatcontext = _context ;
            userManager = _user ;
            signInManager = _signIn ;
        }

        public IActionResult LogIn()
        {
            LoginViewModel loginViewmodel = new LoginViewModel();
            return View(loginViewmodel);
        }

        [HttpPost]
        public async Task <IActionResult>  LogIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
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
                return View(loginViewModel);
            }
            else
            {
                
                return RedirectToAction("Privacy", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Register (RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var admin = new Admin()
                {
                    UserName = registerViewModel.UserName,
                    firstName = registerViewModel.firstName,
                    lastName = registerViewModel.lastName
                };

                var result = await userManager.CreateAsync(admin, registerViewModel.PassWord);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(admin, false);
                        return RedirectToAction("Index", "Home");
                }

                return View();
            }
            return View();
        }



        public IActionResult CustomerOrderForm()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CustomerOrderForm()
        {

        }

    }


}

