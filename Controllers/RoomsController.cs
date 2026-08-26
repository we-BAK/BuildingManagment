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
    public class RoomsController : Controller
    {
        private readonly DBContext _context;

        public RoomsController(DBContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Floor).ThenInclude(f => f.Building)
                .Include(r => r.RoomStatus)
                .Include(r => r.RoomPrices)
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Floor.BuildingId).ThenBy(r => r.Name)
                .ToListAsync();

            return View(rooms);
        }

        // GET: Rooms/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.Floors = new SelectList(await _context.Floors.Where(f => !f.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.Statuses = new SelectList(await _context.RoomStatues.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name");
            return View("CreateEdit", new RoomFormViewModel());
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
                ViewBag.Floors = new SelectList(await _context.Floors.Where(f => !f.IsDeleted).ToListAsync(), "Id", "Name", model.FloorId);
                ViewBag.Statuses = new SelectList(await _context.RoomStatues.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name", model.RoomStatueId);
                return View("CreateEdit", model);
            }

            var defaultFloor = await _context.Floors.FirstOrDefaultAsync(f => !f.IsDeleted && f.BuildingId == model.BuildingId);

            var room = new Room
            {
                Name = model.RoomNumber,
                FloorId = model.FloorId ?? defaultFloor?.Id ?? 1,
                UserId = 1,
                RoomStatusId = model.RoomStatueId,
                SizeInm2 = (int)model.Size,
                Description = model.Description ?? "Standard Room Unit",
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            if (model.DefaultRent > 0)
            {
                var roomPrice = new RoomPrice
                {
                    RoomId = room.Id,
                    PricePerM2 = (double)model.DefaultRent,
                    AppliedDate = DateOnly.FromDateTime(DateTime.Today),
                    IsActive = true,
                    IsDeleted = false
                };
                _context.RoomPrices.Add(roomPrice);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"Room '{room.Name}' created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.Include(r => r.Floor).FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            if (room == null) return NotFound();

            var latestPrice = await _context.RoomPrices.FirstOrDefaultAsync(p => p.RoomId == id && !p.IsDeleted);

            var model = new RoomFormViewModel
            {
                Id = room.Id,
                BuildingId = room.Floor?.BuildingId ?? 1,
                FloorId = room.FloorId,
                RoomNumber = room.Name ?? string.Empty,
                Bedrooms = 1,
                Bathrooms = 1,
                Size = room.SizeInm2,
                RoomStatueId = room.RoomStatusId,
                DefaultRent = (decimal)(latestPrice?.PricePerM2 ?? 0),
                IsActive = room.IsActive
            };

            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
            ViewBag.Floors = new SelectList(await _context.Floors.Where(f => !f.IsDeleted).ToListAsync(), "Id", "Name", model.FloorId);
            ViewBag.Statuses = new SelectList(await _context.RoomStatues.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name", model.RoomStatueId);
            return View("CreateEdit", model);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
                ViewBag.Floors = new SelectList(await _context.Floors.Where(f => !f.IsDeleted).ToListAsync(), "Id", "Name", model.FloorId);
                ViewBag.Statuses = new SelectList(await _context.RoomStatues.Where(s => !s.IsDeleted).ToListAsync(), "Id", "Name", model.RoomStatueId);
                return View("CreateEdit", model);
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) return NotFound();

            if (model.FloorId.HasValue)
            {
                room.FloorId = model.FloorId.Value;
            }
            room.Name = model.RoomNumber;
            room.RoomStatusId = model.RoomStatueId;
            room.SizeInm2 = (int)model.Size;
            room.IsActive = model.IsActive;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Room '{room.Name}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Floor).ThenInclude(f => f.Building)
                .Include(r => r.RoomStatus)
                .Include(r => r.RoomPrices)
                .Include(r => r.RoomRentals).ThenInclude(rr => rr.Tenant)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (room == null) return NotFound();

            var viewModel = new RoomDetailsViewModel
            {
                Room = room,
                RentalHistory = room.RoomRentals.Where(rr => !rr.IsDeleted).OrderByDescending(rr => rr.StartDate).ToList(),
                PriceHistory = room.RoomPrices.Where(rp => !rp.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        // GET: Rooms/Prices/5
        public async Task<IActionResult> Prices(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomPrices)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (room == null) return NotFound();

            var viewModel = new RoomPriceViewModel
            {
                RoomId = room.Id,
                RoomNumber = room.Name ?? string.Empty,
                PriceHistory = room.RoomPrices.Where(rp => !rp.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        // POST: Rooms/SavePrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePrice(RoomPriceViewModel model)
        {
            if (model.Price <= 0)
            {
                TempData["ErrorMessage"] = "Price must be greater than 0.";
                return RedirectToAction(nameof(Prices), new { id = model.RoomId });
            }

            var roomPrice = new RoomPrice
            {
                RoomId = model.RoomId,
                PricePerM2 = (double)model.Price,
                AppliedDate = DateOnly.FromDateTime(DateTime.Today),
                IsActive = true,
                IsDeleted = false
            };

            _context.RoomPrices.Add(roomPrice);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Room pricing plan updated successfully!";
            return RedirectToAction(nameof(Prices), new { id = model.RoomId });
        }
    }
}
