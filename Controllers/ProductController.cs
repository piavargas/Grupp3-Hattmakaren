using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id); // Hitta produkten med det angivna ID:et
            if (product == null)
            {
                return NotFound(); // Om produkten inte hittas, returnera NotFound-resultat
            }

            _context.Products.Remove(product); // Ta bort produkten från databasen
            _context.SaveChanges(); // Spara ändringar i databasen
            return RedirectToAction("Index", "Home"); // Omdirigera till listan över produkter
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product product = _context.Products.Find(id);
           // Product product = _context.Produkter.FirstOrDefault(prod => prod.Id.Equals(id));
            
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            // Hämta den befintliga produkten från databasen
            var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                // Uppdatera egenskaperna med nya värden
                existingProduct.productName = product.productName;
                existingProduct.description = product.description;
                existingProduct.size = product.size;
                existingProduct.price = product.price;

                // Uppdatera den befintliga produkten i databasen
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }



    }
}
