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
            // Hämta enquiry och tillhörande data
            var order = _context.Orders
            //var enquiry = _context.Enquiries
            .Include(o => o.Enquiry)
            // Avkommentera om vi ska ha information om kunden i ordersummary:
            //.Include(e => e.Customer)
            //.Include(o => o.Address)

            .FirstOrDefault(e => e.OrderId == orderId);

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
                //doc.Add(new Paragraph($"Enquiry ID: {enquiry.EnquiryId}"));
                doc.Add(new Paragraph($"Head Size: {order.Enquiry.headSize}"));
                //doc.Add(new Paragraph($"Consent to Modify Existing Hat: {(enquiry.consentHat ? "Yes" : "No")}"));
                doc.Add(new Paragraph($"Description: {order.Enquiry.description}"));
                doc.Add(new Paragraph($"Fabric Material: {order.Enquiry.fabricMaterial}"));
                doc.Add(new Paragraph($"Special Effect Materials: {order.Enquiry.specialEffectMaterials}"));
                doc.Add(new Paragraph($"Color: {order.Enquiry.color}"));
                doc.Add(new Paragraph($"Text on Hat: {order.Enquiry.textOnHat}"));
                doc.Add(new Paragraph($"Font: {order.Enquiry.font}"));

                doc.Close();

                byte[] pdfBytes = memoryStream.ToArray();

                return File(pdfBytes, "application/pdf", "enquiry_details.pdf");
            }
        }

    }
}