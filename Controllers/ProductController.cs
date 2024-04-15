using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

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

        [HttpPost]
        public IActionResult AddProduct(Product produktObjekt, IFormFile ImagePath)
        {
            if (ModelState.IsValid)
            {
                // Ladda upp bildfilen om den har valts
                if (ImagePath != null && ImagePath.Length > 0)
                {
                    // Skapa en unik filnamn för den uppladdade bilden
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);

                    // Ange sökvägen där bilden ska sparas i wwwroot/images
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // Läs in bildfilen och spara den på servern
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImagePath.CopyTo(stream);
                    }

                    // Uppdatera sökvägen till bilden i produktobjektet
                    produktObjekt.ImagePath = "/images/" + fileName;
                }

                // Lägg till produkten i databasen
                _context.Add(produktObjekt);
                _context.SaveChanges(); // Spara ändringar i databasen
                return RedirectToAction("Index", "Home"); // Omdirigera till listan över produkter
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
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            // Hämta den befintliga produkten från databasen
            var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (ModelState.IsValid)
            {
                // Uppdatera egenskaperna med nya värden
                existingProduct.productName = product.productName;
                existingProduct.description = product.description;
                existingProduct.size = product.size;
                existingProduct.price = product.price;

                // Uppdatera den befintliga produkten i databasen
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }
    }
}
