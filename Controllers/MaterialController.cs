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
            return View(materials);
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

        [HttpPost]
        public async Task<IActionResult> IncreaseMaterial(Material model)
        {
            var material = await _hatcontext.Materials.FindAsync(model.materialId);
            if (material != null)
            {
                material.quantity = model.quantity;
                _hatcontext.Update(material);
            }
            return RedirectToAction("Material");

        }

        public IActionResult AddNewMaterial()
        {
            return View();
        }
    }
}
