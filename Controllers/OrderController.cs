using Microsoft.AspNetCore.Mvc;
using Grupp3Hattmakaren;

namespace Grupp3Hattmakaren.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly PdfService _pdfService;
        private readonly ViewRenderService _viewRenderService;

        public OrderController(OrderService orderService,PdfService pdfService, ViewRenderService viewRenderService)
        {
            _orderService = orderService;
            _pdfService = pdfService;
            _viewRenderService = viewRenderService;
        }

        public IActionResult OrderSummary(int orderId)
        {
            var orderSummary = _orderService.GetOrderSummary(orderId);
            if (orderSummary == null)
            {
                return NotFound("Order not found.");
            }

            // Here, you could return a view or directly a PDF file as shown in the previous step
            return View(orderSummary);
        }

        public IActionResult DownloadOrderSummaryPdf(int orderId)
        {
            var model = _orderService.GetOrderSummary(orderId);
            if (model == null)
            {
                return NotFound();
            }

            var html = _viewRenderService.RenderToString("OrderSummary", model);
            var file = _pdfService.GeneratePdf(html);

            return File(file, "application/pdf", $"OrderSummary-{model.OrderId}.pdf");
        }

    }

}
