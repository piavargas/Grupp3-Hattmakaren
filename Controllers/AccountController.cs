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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(HatContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _hatcontext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult LogIn()
        {
            LoginViewModel loginViewmodel = new LoginViewModel();
            return View(loginViewmodel);
        }

        //[HttpPost]
        //public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(
        //            loginViewModel.UserName,
        //            loginViewModel.PassWord,
        //            isPersistent: true,
        //            lockoutOnFailure: false
        //            );

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Wrong username/password.");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Privacy", "Home");
        //    }

        //    return View(loginViewModel);
        // }

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
                    var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
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

        private async Task EnsureRolesCreated()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            await EnsureRolesCreated();

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName,
                firstName = model.firstName,
                lastName = model.lastName
                };

                if (model.userRole == "Admin")
                {
                    string[] validEmployerCodes = { "0192837465", "1029384756", "2143658709" };
                    if (!validEmployerCodes.Contains(model.employerCode))
                    {
                        ModelState.AddModelError("employerCode", "Invalid employer code for admin registration.");
                        return View(model);
                    }
                    var admin = new Admin { UserName = model.UserName, firstName = model.firstName, lastName = model.lastName, employerCode = model.employerCode };
                    var result = await _userManager.CreateAsync(admin, model.PassWord);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "Admin");
                        await _signInManager.SignInAsync(admin, isPersistent: false);

                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else if (model.userRole == "Customer")
                {
                    var customer = new Customer { UserName = model.UserName, firstName = model.firstName, lastName = model.lastName, headSize = model.headSize };
                    var result = await _userManager.CreateAsync(customer, model.PassWord);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(customer, "Customer");
                        await _signInManager.SignInAsync(customer, isPersistent: false);



    }
}