using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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



    }
}
