using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using BMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class TenantsController : Controller
    {
        private readonly DBContext _context;

        public TenantsController(DBContext context)
        {
            _context = context;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            var tenants = await _context.Tenants
                .Include(t => t.RoomRentals)
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return View(tenants);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View("CreateEdit", new TenantFormViewModel());
        }

        // POST: Tenants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantFormViewModel model)
        {
            if (!ModelState.IsValid) return View("CreateEdit", model);

            var tenant = new Tenant
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName ?? string.Empty,
                LastName = model.LastName,
                IdentityCardNumber = model.IdentityCardNumber,
                Email = model.Email,
                Phone = model.Phone,
                TenantTypeId = model.TenantTypeId ?? 1,
                SexId = 1,
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Tenant '{tenant.FirstName} {tenant.LastName}' registered successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null || tenant.IsDeleted) return NotFound();

            var model = new TenantFormViewModel
            {
                Id = tenant.Id,
                FirstName = tenant.FirstName ?? string.Empty,
                MiddleName = tenant.MiddleName ?? string.Empty,
                LastName = tenant.LastName ?? string.Empty,
                IdentityCardNumber = tenant.IdentityCardNumber ?? string.Empty,
                Email = tenant.Email ?? string.Empty,
                Phone = tenant.Phone ?? string.Empty,
                IsActive = tenant.IsActive
            };

            return View("CreateEdit", model);
        }

        // POST: Tenants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TenantFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View("CreateEdit", model);

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null || tenant.IsDeleted) return NotFound();

            tenant.FirstName = model.FirstName;
            tenant.MiddleName = model.MiddleName ?? string.Empty;
            tenant.LastName = model.LastName;
            tenant.IdentityCardNumber = model.IdentityCardNumber;
            tenant.Email = model.Email;
            tenant.Phone = model.Phone;
            tenant.IsActive = model.IsActive;

            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Tenant profile updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Tenants/Terminations
        public async Task<IActionResult> Terminations()
        {
            var terminations = await _context.RentalAgreementTerminations
                .Include(t => t.RentalAgreement).ThenInclude(r => r.Room)
                .Include(t => t.RentalAgreement).ThenInclude(r => r.Tenant)
                .Include(t => t.Status)
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();

            return View(terminations);
        }

        // GET: Tenants/RequestTermination
        public async Task<IActionResult> RequestTermination()
        {
            ViewBag.ActiveRentals = await _context.RoomRentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => r.IsActive && !r.IsDeleted)
                .ToListAsync();

            return View(new TerminationRequestViewModel());
        }

        // POST: Tenants/RequestTermination
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestTermination(TerminationRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ActiveRentals = await _context.RoomRentals
                    .Include(r => r.Room)
                    .Include(r => r.Tenant)
                    .Where(r => r.IsActive && !r.IsDeleted)
                    .ToListAsync();
                return View(model);
            }

            var termination = new RentalAgreementTermination
            {
                RentalAgreementId = model.RoomRentalId,
                TerminationDate = model.RequestedTerminationDate,
                Reason = model.Reason,
                StatusId = 1, // Pending
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            _context.RentalAgreementTerminations.Add(termination);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Rental Agreement Termination request submitted!";
            return RedirectToAction(nameof(Terminations));
        }

        // GET: Tenants/TerminationApproval/5
        public async Task<IActionResult> TerminationApproval(int id)
        {
            var termination = await _context.RentalAgreementTerminations
                .Include(t => t.RentalAgreement).ThenInclude(r => r.Room)
                .Include(t => t.RentalAgreement).ThenInclude(r => r.Tenant)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (termination == null) return NotFound();

            var viewModel = new TerminationApprovalViewModel
            {
                TerminationId = termination.Id,
                TenantName = $"{termination.RentalAgreement?.Tenant?.FirstName} {termination.RentalAgreement?.Tenant?.LastName}",
                RoomNumber = termination.RentalAgreement?.Room?.RoomNumber ?? "N/A",
                RequestedDate = termination.TerminationDate,
                Reason = termination.Reason ?? "N/A"
            };

            return View(viewModel);
        }

        // POST: Tenants/ProcessTermination
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessTermination(int terminationId, bool approved, string approvalNotes)
        {
            var termination = await _context.RentalAgreementTerminations
                .Include(t => t.RentalAgreement).ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(t => t.Id == terminationId && !t.IsDeleted);

            if (termination == null) return NotFound();

            termination.StatusId = approved ? 2 : 3; // 2: Approved, 3: Rejected
            termination.Reason = (termination.Reason + " | Inspection Remarks: " + approvalNotes).Trim();

            if (approved && termination.RentalAgreement != null)
            {
                termination.RentalAgreement.IsActive = false;
                if (termination.RentalAgreement.Room != null)
                {
                    termination.RentalAgreement.Room.RoomStatueId = 1; // Vacant
                }
            }

            var approvalRecord = new RentalTerminationApproval
            {
                TerminationId = termination.Id,
                Remarks = approvalNotes,
                IsApproved = approved,
                ApprovalDate = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            _context.RentalTerminationApprovals.Add(approvalRecord);
            _context.RentalAgreementTerminations.Update(termination);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = approved ? "Termination request approved & property unit vacated." : "Termination request rejected.";
            return RedirectToAction(nameof(Terminations));
        }
    }
}
