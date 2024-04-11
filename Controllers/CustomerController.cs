using Microsoft.AspNetCore.Mvc;

namespace Grupp3Hattmakaren.Controllers
{
    public class CustomerController : Controller
    {
      
        public IActionResult CustomerOrderForm()
        {
            return View();
        }

    }
}
