using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Grupp3Hattmakaren.Controllers
{
    public class ShippingBillController : Controller
    {
        private readonly HatContext _hatcontext;

        public ShippingBillController(HatContext context)
        {
            _hatcontext = context;
        }

        // GET: ShippingBill/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Inget ID = Returnera HTTP 400 Bad Request
            }

            //LINQ query för att extrahera den data vi behöver från relevanta tabeller
            //Lagras sedan i fälten från ShippingBillViewModel
            var shippingBillDetails = _hatcontext.ShippingBills
                .Where(sb => sb.ShippingBillId == id)
                .Select(sb => new ShippingBillViewModel
                {
                    productCode = sb.productCode,
                    customerFullName = sb.order.Customer.firstName + " " + sb.order.Customer.lastName,
                    addressDetails = sb.order.Address.streetName + ", " + sb.order.Address.zipCode + " " + sb.order.Address.cityName + ", " + sb.order.Address.countryName
                }).FirstOrDefault();



            if (shippingBillDetails == null)
            {
                return NotFound(); // Ingen fraktsedel matchar givet ID = Returnera HTTP 404 Not Found
            }

            return View(shippingBillDetails); // Skicka data till view
        }
    }


}
