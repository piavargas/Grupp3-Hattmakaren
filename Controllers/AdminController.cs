using Microsoft.AspNetCore.Mvc;

namespace Grupp3Hattmakaren.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
