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
    public class FloorsController : Controller
    {
        private readonly DBContext _context;

        public FloorsController(DBContext context)
        {
            _context = context;
        }

        // GET: Floors
        public async Task<IActionResult> Index()
        {
            var floors = await _context.Floors
                .Include(f => f.Building)
                .Include(f => f.Rooms)
                .Include(f => f.FloorPrices)
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.BuildingId).ThenBy(f => f.Id)
                .ToListAsync();

            return View(floors);
        }

        // GET: Floors/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            return View("CreateEdit", new FloorFormViewModel());
        }

        // POST: Floors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FloorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
                return View("CreateEdit", model);
            }

            var floor = new Floor
            {
                BuildingId = model.BuildingId,
                Name = model.Name,
                NumberOfRoom = model.FloorNumber.ToString(),
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Floor '{floor.Name}' added successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Floors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var floor = await _context.Floors.FindAsync(id);
            if (floor == null || floor.IsDeleted) return NotFound();

            int.TryParse(floor.NumberOfRoom, out int floorNum);

            var model = new FloorFormViewModel
            {
                Id = floor.Id,
                BuildingId = floor.BuildingId,
                Name = floor.Name ?? string.Empty,
                FloorNumber = floorNum,
                IsActive = floor.IsActive
            };

            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
            return View("CreateEdit", model);
        }

        // POST: Floors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FloorFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
                return View("CreateEdit", model);
            }

            var floor = await _context.Floors.FindAsync(id);
            if (floor == null || floor.IsDeleted) return NotFound();

            floor.BuildingId = model.BuildingId;
            floor.Name = model.Name;
            floor.NumberOfRoom = model.FloorNumber.ToString();
            floor.IsActive = model.IsActive;

            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Floor '{floor.Name}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Floors/Prices/5
        public async Task<IActionResult> Prices(int id)
        {
            var floor = await _context.Floors
                .Include(f => f.Building)
                .Include(f => f.FloorPrices)
                .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);

            if (floor == null) return NotFound();

            var viewModel = new FloorPriceViewModel
            {
                FloorId = floor.Id,
                FloorName = floor.Name ?? string.Empty,
                BuildingName = floor.Building?.Name ?? string.Empty,
                ExistingPrices = floor.FloorPrices.Where(fp => !fp.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        // POST: Floors/SavePrice
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePrice(FloorPriceViewModel model)
        {
            if (model.BasePrice <= 0)
            {
                TempData["ErrorMessage"] = "Base monthly price must be greater than 0.";
                return RedirectToAction(nameof(Prices), new { id = model.FloorId });
            }

            var floorPrice = new FloorPrice
            {
                FloorId = model.FloorId,
                PricePerM2 = (double)model.BasePrice,
                AppliedDate = DateOnly.FromDateTime(DateTime.Today),
                IsActive = true,
                IsDeleted = false
            };

            _context.FloorPrices.Add(floorPrice);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Floor base price updated successfully!";
            return RedirectToAction(nameof(Prices), new { id = model.FloorId });
        }
    }
}
