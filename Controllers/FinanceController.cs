using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS.Controllers
{
    public class FinanceController : Controller
    {
        private readonly DBContext _context;

        public FinanceController(DBContext context)
        {
            _context = context;
        }

        // GET: Finance/Index (Invoices List)
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.InvoiceStatus)
                .Where(i => !i.IsDeleted)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            return View(invoices);
        }

        // GET: Finance/CreateInvoice
        public async Task<IActionResult> CreateInvoice()
        {
            ViewBag.Users = new SelectList(await _context.Users.Where(u => !u.IsDeleted).ToListAsync(), "Id", "FullName");
            ViewBag.InvoiceStatuses = new SelectList(await _context.InvoiceStatuses.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Finance/CreateInvoice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoice(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.IsActive = true;
                invoice.IsDeleted = false;
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Invoice created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = new SelectList(await _context.Users.Where(u => !u.IsDeleted).ToListAsync(), "Id", "FullName", invoice.UserId);
            ViewBag.InvoiceStatuses = new SelectList(await _context.InvoiceStatuses.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name", invoice.InvoiceStatusId);
            return View(invoice);
        }

        // GET: Finance/InvoiceDetails/5
        public async Task<IActionResult> InvoiceDetails(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.User)
                .Include(i => i.InvoiceStatus)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Finance/RoomRentalPayments
        public async Task<IActionResult> RoomRentalPayments()
        {
            var payments = await _context.RoomRentalPayments
                .Include(p => p.PaymentMode)
                .Include(p => p.PaymentType)
                .Include(p => p.RoomRental)
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.PaidDate)
                .ToListAsync();

            return View(payments);
        }
    }
}
