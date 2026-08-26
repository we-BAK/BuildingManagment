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
    public class RoomRentalsController : Controller
    {
        private readonly DBContext _context;

        public RoomRentalsController(DBContext context)
        {
            _context = context;
        }

        // GET: RoomRentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.RoomRentals
                .Include(r => r.Room).ThenInclude(rm => rm.Floor).ThenInclude(f => f.Building)
                .Include(r => r.Tenant)
                .Include(r => r.BusinessArea)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            return View(rentals);
        }

        // GET: RoomRentals/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View("CreateEdit", new RoomRentalFormViewModel());
        }

        // POST: RoomRentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomRentalFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model.RoomId, model.TenantId, model.BusinessAreaId);
                return View("CreateEdit", model);
            }

            var rental = new RoomRental
            {
                RoomId = model.RoomId,
                TenantId = model.TenantId,
                StartDate = model.StartDate,
                TotalPrice = (double)model.MonthlyRent,
                BusinessAreaId = model.BusinessAreaId,
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.RoomRentals.Add(rental);

            // Update room status to Occupied (Id = 2)
            var room = await _context.Rooms.FindAsync(model.RoomId);
            if (room != null)
            {
                room.RoomStatusId = 2;
                _context.Rooms.Update(room);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lease agreement created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: RoomRentals/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rental = await _context.RoomRentals.FindAsync(id);
            if (rental == null || rental.IsDeleted) return NotFound();

            var model = new RoomRentalFormViewModel
            {
                Id = rental.Id,
                RoomId = rental.RoomId,
                TenantId = rental.TenantId,
                StartDate = rental.StartDate,
                MonthlyRent = (decimal)rental.TotalPrice,
                BusinessAreaId = rental.BusinessAreaId,
                IsActive = rental.IsActive
            };

            await PopulateDropdownsAsync(model.RoomId, model.TenantId, model.BusinessAreaId);
            return View("CreateEdit", model);
        }

        // POST: RoomRentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomRentalFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model.RoomId, model.TenantId, model.BusinessAreaId);
                return View("CreateEdit", model);
            }

            var rental = await _context.RoomRentals.FindAsync(id);
            if (rental == null || rental.IsDeleted) return NotFound();

            rental.RoomId = model.RoomId;
            rental.TenantId = model.TenantId;
            rental.StartDate = model.StartDate;
            rental.TotalPrice = (double)model.MonthlyRent;
            rental.BusinessAreaId = model.BusinessAreaId;
            rental.IsActive = model.IsActive;

            _context.RoomRentals.Update(rental);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lease agreement updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: RoomRentals/Terminations
        public async Task<IActionResult> Terminations()
        {
            var terminations = await _context.RentalAgreementTerminations
                .Include(t => t.RoomRental).ThenInclude(r => r.Room)
                .Include(t => t.RoomRental).ThenInclude(r => r.Tenant)
                .Where(t => !t.IsDeleted)
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return View(terminations);
        }

        private async Task PopulateDropdownsAsync(int? selectedRoomId = null, int? selectedTenantId = null, int? selectedBusinessAreaId = null)
        {
            ViewBag.Rooms = new SelectList(await _context.Rooms
                .Include(r => r.Floor).ThenInclude(f => f.Building)
                .Where(r => !r.IsDeleted)
                .Select(r => new { Id = r.Id, Name = $"{r.Floor.Building.Name} - {r.Name}" })
                .ToListAsync(), "Id", "Name", selectedRoomId);

            // Accesses t.Name directly from your Tenant entity
            ViewBag.Tenants = new SelectList(await _context.Tenants
                .Where(t => !t.IsDeleted)
                .Select(t => new { Id = t.Id, Name = t.Name })
                .ToListAsync(), "Id", "Name", selectedTenantId);

            ViewBag.BusinessAreas = new SelectList(await _context.BusinessAreas
                .Where(b => !b.IsDeleted)
                .Select(b => new { Id = b.Id, Name = b.Name })
                .ToListAsync(), "Id", "Name", selectedBusinessAreaId);
        }
    }
}