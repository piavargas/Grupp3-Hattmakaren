using Grupp3Hattmakaren.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;

namespace Grupp3Hattmakaren.Controllers
{
    public class MessageController : Controller
    {
        private UserManager<User> _userManager;
        private object _logger;
        private readonly HatContext _context;

        public MessageController(UserManager<User> userMngr, HatContext _hatcontext)
        {
            _userManager = userMngr;
            _context = _hatcontext;
        }

        public IActionResult SendOffer()
        {
            List<User> users;
            Message message = new Message();
            users = _context.Users.ToList();
            ViewBag.Users = users;
            return View();
        }

        [HttpPost]
        public IActionResult SendOffer(SendMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Message message = new Message
                {
                    //MessageId = model.MessageId,
                    text = model.messageText,
                    sender = model.sender,
                    Id = model.UserId,
                };

                _context.Add(message);
                _context.SaveChanges();
                TempData["MessageSent"] = "Message has been sent.";
                return RedirectToAction("SendOffer", "Message");
            }

            var users = _context.Users.ToList();
            ViewBag.Users = users;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerMessages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var receivedMessages = _context.Messages
                    .Where(message => message.Id == userId)
                    .Select(message => new SendMessageViewModel
                    {
                        MessageId = message.MessageId,
                        messageText = message.text,
                        sender = message.sender,
                        UserId = message.Id
                    })
                    .ToList();

                return View(receivedMessages);
            }

            return View(new List<SendMessageViewModel>());
        }

        public object Get_logger()
        {
            return _logger;
        }


        public IActionResult MakePayment()
        {
            TempData["PaymentMessage"] = "Thank you for your payment. We will process your order as soon as possible.";
            TempData["Message"] = "A customer has accepted the offer, please handle the order as soon as possible.";
            return RedirectToAction("CustomerMessages", "Message");
        }

        public IActionResult DeclineOffer()
        {
            TempData["OfferDeclined"] = "Thank you for your time! Not satisfied? Make a new enquiry!";
            TempData["DeclineMessage"] = "A customer has declined the offer. ";
            return RedirectToAction("CustomerMessages", "Message");
        }


        //[HttpPost]
        //public IActionResult DeclineOffer(int messageId)
        //{
        //    var messageToRemove = _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
        //    if (messageToRemove != null)
        //    {
        //        _context.Messages.Remove(messageToRemove);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("CustomerMessages");
        //}


    }
}
