using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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

                // Uppdatera bilden endast om en ny bild har valts
                if (Request.Form.Files.Count > 0)
                {
                    var ImagePath = Request.Form.Files[0];
                    if (ImagePath != null && ImagePath.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        // Ta bort den befintliga bilden om det finns en och ersätt den med den nya bilden
                        if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", Path.GetFileName(existingProduct.ImagePath));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImagePath.CopyTo(stream);
                        }

                        existingProduct.ImagePath = "/images/" + fileName;
                    }
                }

                // Uppdatera den befintliga produkten i databasen
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        [HttpPost]
        //Lägger till en produkt i kundvagnen
        public IActionResult AddToCart(int productId) 
        {
            
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var cart = _context.ShoppingCarts.FirstOrDefault(sc => sc.customerId == user.Id);

            //Om kunden inte redan har en kundvagn så skapas den här
            if(cart == null && user != null) 
            {
                cart = new ShoppingCart
                {
                    customerId = user.Id
                };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();
            }
            
            if(product != null) 
            {
                List<ProductShoppingCart> pShoppingCartList = _context.ProductShoppingCarts.ToList();

                //Kollar ifall man redan lagt till en av samma produkt och i sådana fall ökar den mängden med ett
                foreach(var pShoppingCart in pShoppingCartList) 
                { 
                    if(pShoppingCart.productId == productId) 
                    {
                        pShoppingCart.quantity++;
                        _context.Update(pShoppingCart);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }

                //Skapar produkt till kundvagn
                var cartItem = new ProductShoppingCart
                {
                    productId = productId,
                    shoppingCartId = cart.Id,
                    quantity = 1
                };

                _context.ProductShoppingCarts.Add(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
