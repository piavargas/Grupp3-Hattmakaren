﻿using Grupp3Hattmakaren.Models;
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

        //[HttpPost]
        //public IActionResult MarkAsRead(int messageid)
        //{
        //    var message = _context.Messages.Find(messageid);

        //    if (message != null && !message.isRead)
        //    {
        //        message.isRead = true;
        //        _context.SaveChanges();
        //    }

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = _userManager.FindByIdAsync(userId).Result;

        //    if (user != null)
        //    {
        //        var receivedMessageQuery = _context.Messages
        //            .Where(msg => msg.Id == userId); // Query messages for the current user

        //        List<SendMessageViewModel> messageViewModels = receivedMessageQuery
        //            .Select(msg => new SendMessageViewModel
        //            {
        //                MessageId = msg.MessageId,
        //                messageText = msg.text,
        //                sender = msg.sender,
        //                isRead = msg.isRead,
        //                UserId = msg.Id
        //            })
        //            .ToList();

        //        ViewBag.UnreadMessagesCount = receivedMessageQuery.Count(msg => !msg.isRead);

        //        return View("SeeMessages", messageViewModels);
        //    }

        //    return View("SeeMessages", new List<SendMessageViewModel>());
        //}


    }
}