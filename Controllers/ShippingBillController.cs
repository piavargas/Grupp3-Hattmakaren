using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Grupp3Hattmakaren.Controllers
{
    public class ShippingBillController : Controller
    {
        private readonly HatContext _hatcontext;
        private readonly PDFController _pdfController;
        private readonly ViewRenderService _viewRenderService;

        public ShippingBillController(HatContext context, PDFController pdfController, ViewRenderService viewRenderService)
        {
            _hatcontext = context;
            _pdfController = pdfController;
            _viewRenderService = viewRenderService;
        }

        // GET: ShippingBill/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Inget ID = Returnera HTTP 400 Bad Request
            }

            //LINQ query för att extrahera den data vi behöver från relevanta tabeller
           // Lagras sedan i fälten från ShippingBillViewModel
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


        // Metod för att ladda ner PDF (inför utskrivning)
        public IActionResult DownloadPdf(int id)
        {
            //Använder metoden Details som ligger längre upp i denna klass
            var model = Details(id);
            if (model == null)
            {
                return NotFound(); // Make sure to handle cases where the model is not found.
            }

            //Generera HTML string från Razor view kallad "PrintShippingBill"
            var html = _viewRenderService.RenderToString("PrintShippingBill", model);

            //Generera PDF från HTML string
            var file = _pdfController.GeneratePdf(html);

            //Returnera filen som låter webläsaren ladda ner den
            return File(file, "application/pdf", "ShippingBill.pdf");
        }


    }


}
