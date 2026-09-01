using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using BMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .Include(t => t.Building)
                .Include(t => t.TenantType)
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return View(tenants);
        }

        // GET: Tenants/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View("CreateEdit", new TenantFormViewModel());
        }

        // POST: Tenants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model.TenantTypeId);
                return View("CreateEdit", model);
            }

            var tenant = new Tenant
            {
                Name = $"{model.FirstName} {model.LastName}".Trim(), // Maps full name to Name
                Contact = model.Phone ?? model.Email ?? string.Empty, // Maps contact info to Contact
                Description = model.CompanyName ?? "Individual Tenant",
                TenantTypeId = model.TenantTypeId ?? 1,
                BuildingId = 1, // Set your default or selected BuildingId
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Tenant '{tenant.Name}' created successfully!";
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
                FirstName = tenant.Name,
                Phone = tenant.Contact,
                CompanyName = tenant.Description,
                TenantTypeId = tenant.TenantTypeId,
                IsActive = tenant.IsActive
            };

            await PopulateDropdownsAsync(tenant.TenantTypeId);
            return View("CreateEdit", model);
        }

        // POST: Tenants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TenantFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model.TenantTypeId);
                return View("CreateEdit", model);
            }

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null || tenant.IsDeleted) return NotFound();

            tenant.Name = $"{model.FirstName} {model.LastName}".Trim();
            tenant.Contact = model.Phone ?? model.Email ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(model.CompanyName)) tenant.Description = model.CompanyName;
            if (model.TenantTypeId.HasValue) tenant.TenantTypeId = model.TenantTypeId.Value;
            tenant.IsActive = model.IsActive;

            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Tenant '{tenant.Name}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(int? selectedTenantTypeId = null)
        {
            ViewBag.TenantTypes = new SelectList(await _context.TenantTypes
                .Where(tt => !tt.IsDeleted)
                .ToListAsync(), "Id", "Name", selectedTenantTypeId);

            ViewBag.Buildings = new SelectList(await _context.Buildings
                .Where(b => !b.IsDeleted)
                .ToListAsync(), "Id", "Name");
        }
    }
}