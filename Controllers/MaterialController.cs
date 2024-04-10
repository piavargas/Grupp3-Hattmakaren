using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;

namespace Grupp3Hattmakaren.Controllers
{
    public class MaterialController : Controller
    {
        private readonly HatContext _hatcontext;

        public MaterialController(HatContext _context)
        {
            _hatcontext = _context;
        }
        public IActionResult Material()
        {
            List<Material> materials = _hatcontext.Materials.ToList();
            ViewBag.MaterialList = materials;  
            return View();
        }

        [HttpPost]
        public IActionResult Material(Material material) 
        {
            var newMaterial = new Material() 
            { 
                name = material.name,
                supplier = material.supplier,
                price = material.price,
                quantity = material.quantity
            };

            _hatcontext.Add(newMaterial);
            _hatcontext.SaveChanges();

            return RedirectToAction("Material");
        }



    }
}
