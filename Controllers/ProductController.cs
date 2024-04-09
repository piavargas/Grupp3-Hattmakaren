using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;

namespace Grupp3Hattmakaren.Controllers
{
    public class ProductController : Controller
    {
        private readonly HatContext _context;

        public ProductController(HatContext context)
        {
           
            _context = context;
        }
     

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() 
        { 
        Product product = new Product();
            return RedirectToAction("Index", "Home");        
          }
        [HttpPost]
        public IActionResult Add(Product produktObjekt)
        {
            if (ModelState.IsValid) // Kontrollera om modellen är giltig
            {
                _context.Add(produktObjekt); // Lägg till produkten i databasen
                _context.SaveChanges(); // Spara ändringar i databasen
                return RedirectToAction("Index" , "Home"); // Omdirigera till listan över produkter
            }
            // Om modellen inte är giltig, visa samma vy med felmeddelanden
            return View(produktObjekt);
        }
       
       

    }
}
