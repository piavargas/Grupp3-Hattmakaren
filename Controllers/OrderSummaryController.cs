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
            var order = _context.Orders.Include(o => o.Customer).Include(o => o.Address).FirstOrDefault(o => o.OrderId == orderId);

            var shippingBill = new ShippingBill
            {
                // Fyll i fraktsedelsinfo
                //OrderId = order.OrderId,
                productCode = "6504 00 00"
            };

            //var orderSummary = new OrderSummary
            //{
            //    // Fyll i fraktsedelsinfo
            //    //OrderId = order.OrderId,
            //    productCode = "6504 00 00"
            //};

            _context.ShippingBills.Add(shippingBill);
            _context.SaveChanges();

            // Skapa en ny PDF-fil
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                //PDF-fil med fraktsedelsinformation
                doc.Add(new Paragraph($"Shipping Bill ID: {shippingBill.ShippingBillId}"));
                doc.Add(new Paragraph($"Product Code: {shippingBill.productCode}"));
                //doc.Add(new Paragraph($"Order ID: {order.OrderId}"));
                //doc.Add(new Paragraph($"Customer Name: {order.Customer.firstName} {order.Customer.lastName}"));
                //doc.Add(new Paragraph($"Address: {order.Address.streetName}, {order.Address.zipCode}, {order.Address.countryName}"));

                doc.Close();

                byte[] pdfBytes = memoryStream.ToArray();

                return File(pdfBytes, "application/pdf", "shipping_label.pdf");
            }
        }

    }
}