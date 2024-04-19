using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Grupp3Hattmakaren.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;

namespace Grupp3Hattmakaren.Controllers
{
    public class OrderSummaryController : Controller
    {
        private readonly HatContext _context;

        public OrderSummaryController(HatContext context)
        {
            _context = context;
        }

        public IActionResult OrderList()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }


        public IActionResult PrintOrderSummary(int orderId)
        {

            // Hämta ordern och tillhörande data
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Address)
                .Include(o => o.products)  // Kollektion av produkter i en order 
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }



            // Skapa en PDF
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                // Lägg till orderinformation i PDF-filen
                doc.Add(new Paragraph($"Order ID: {order.OrderId}"));
                doc.Add(new Paragraph($"Customer Name: {order.Customer.firstName} {order.Customer.lastName}"));
                doc.Add(new Paragraph($"Address: {order.Address.streetName}, {order.Address.zipCode}, {order.Address.countryName}"));

                //// Information om varje produkt i ordern
                //foreach (var item in order.products)
                //{
                //    doc.Add(new Paragraph($"Product: {item.productName}, Price: {item.price}"));
                //}

                // Information om varje produkt i ordern
                foreach (var item in order.products)
                {
                    string materialNames = item.materials != null && item.materials.Any()
                        ? string.Join(", ", item.materials.Select(m => m.name))
                        : "No materials";

                    doc.Add(new Paragraph($"Product: {item.productName}, Materials: {materialNames}, Price: {item.price}"));
                }


                //Avslutningsvis lägger vi till det totala priset
                doc.Add(new Paragraph($"Total Price: {order.price}"));



                doc.Close();

                byte[] pdfBytes = memoryStream.ToArray();

                return File(pdfBytes, "application/pdf", "order_summary.pdf");
            }
        }

    }
}