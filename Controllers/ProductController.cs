using Microsoft.AspNetCore.Mvc;

namespace Grupp3Hattmakaren.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
