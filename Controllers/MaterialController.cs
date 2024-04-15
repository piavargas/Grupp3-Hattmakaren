using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewMaterial(Material material) 
        {
            if (ModelState.IsValid)
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseMaterial(int materialId, int quantity)
        {
            var material = await _hatcontext.Materials.FindAsync(materialId);
            if (material != null)
            {
                material.quantity += quantity;
                _hatcontext.Update(material);
                _hatcontext.SaveChanges();
            }
            return RedirectToAction("Material");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteMaterial(int materialId)
        {
            var material = await _hatcontext.Materials.FindAsync(materialId);
            if (material != null)
            {
                _hatcontext.Remove(material);
                _hatcontext.SaveChanges();
            }
            return RedirectToAction("Material");

        }


        public IActionResult AddNewMaterial()
        {
            return View();
        }
    }
}
