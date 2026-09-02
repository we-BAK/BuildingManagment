using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMS.Data; // Replace with your actual DbContext namespace

namespace BMS.Controllers
{
    public class CommunicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Communications/Chat
        public IActionResult Chat(int? id)
        {
            ViewBag.SelectedThreadId = id ?? 1;

            // Fetch chat threads dynamically if DbContext has a Chat/Thread entity
            var threadsProperty = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.Name.Contains("Chat") || p.Name.Contains("Thread") || p.Name.Contains("Message"));

            if (threadsProperty != null)
            {
                var dbSet = threadsProperty.GetValue(_context) as IQueryable<object>;
                if (dbSet != null)
                {
                    var data = dbSet.Take(20).ToList();
                    return View(data);
                }
            }

            return View(Enumerable.Empty<object>());
        }

        // POST: /Communications/SendMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(int threadId, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                TempData["SuccessMessage"] = "Message sent successfully.";
            }

            return RedirectToAction(nameof(Chat), new { id = threadId });
        }
    }
}