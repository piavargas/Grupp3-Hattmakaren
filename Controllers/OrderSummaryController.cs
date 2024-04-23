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


        public IActionResult PrintOrderSummary(int enquiryId)
        {
            // Hämta enquiry och tillhörande data
            var enquiry = _context.Enquiries
            // Avkommentera om vi ska ha information om kunden i ordersummary:
            //.Include(e => e.Customer)
            //.Include(o => o.Address)

            .FirstOrDefault(e => e.EnquiryId == enquiryId);

            if (enquiry == null)
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
                doc.Add(new Paragraph($"Head Size: {enquiry.headSize}"));
                doc.Add(new Paragraph($"Consent to Modify Existing Hat: {(enquiry.consentHat ? "Yes" : "No")}"));
                doc.Add(new Paragraph($"Description: {enquiry.description}"));
                doc.Add(new Paragraph($"Fabric Material: {enquiry.fabricMaterial}"));
                doc.Add(new Paragraph($"Special Effect Materials: {enquiry.specialEffectMaterials}"));
                doc.Add(new Paragraph($"Color: {enquiry.color}"));
                doc.Add(new Paragraph($"Text on Hat: {enquiry.textOnHat}"));
                doc.Add(new Paragraph($"Font: {enquiry.font}"));

                doc.Close();

                byte[] pdfBytes = memoryStream.ToArray();

                return File(pdfBytes, "application/pdf", "enquiry_details.pdf");
            }
        }

    }
}