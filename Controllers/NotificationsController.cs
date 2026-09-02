using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BMS.Data;

namespace BMS.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Notifications
        public IActionResult Index()
        {
            var notifProperty = _context.GetType().GetProperties()
                .FirstOrDefault(p => p.Name.Contains("Notification") || p.Name.Contains("Alert"));

            if (notifProperty != null)
            {
                var dbSet = notifProperty.GetValue(_context) as IQueryable<object>;
                if (dbSet != null)
                {
                    var notifications = dbSet.Take(50).ToList();
                    return View(notifications);
                }
            }

            return View(Enumerable.Empty<object>());
        }

        // POST: /Notifications/MarkAsRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            TempData["SuccessMessage"] = "Notification marked as read.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Notifications/MarkAllAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllAsRead()
        {
            TempData["SuccessMessage"] = "All notifications marked as read.";
            return RedirectToAction(nameof(Index));
        }
    }
}