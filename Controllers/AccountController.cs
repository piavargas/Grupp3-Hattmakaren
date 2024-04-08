using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        [HttpPost]
        public async Task <IActionResult>  LogIn(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    admin.UserName,
                    admin.PasswordHash,

                    isPersistent: true,
                    lockoutOnFailure: false
                    );
                return RedirectToAction("Index","Home");
            }
            else
            {
                // ModelState är inte giltig, visa felmeddelanden
                return View(admin);
            }
        }

    }


}

