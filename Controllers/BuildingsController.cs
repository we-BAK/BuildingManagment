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
    public class BuildingsController : Controller
    {
        private readonly DBContext _context;

        public BuildingsController(DBContext context)
        {
            _context = context;
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {
            var buildings = await _context.Buildings
                .Include(b => b.BuildingType)
                .Include(b => b.Location)
                .Include(b => b.Floors).ThenInclude(f => f.Rooms)
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.Id)
                .ToListAsync();

            return View(buildings);
        }

        // GET: Buildings/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.BuildingTypes = new SelectList(await _context.BuildingTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name");
            return View("CreateEdit", new BuildingFormViewModel());
        }

        // POST: Buildings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BuildingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BuildingTypes = new SelectList(await _context.BuildingTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingTypeId);
                return View("CreateEdit", model);
            }

            var building = new Building
            {
                Name = model.Name,
                BuildingTypeId = model.BuildingTypeId,
                CityId = model.CityId ?? 1,
                UseTypeId = model.UseTypeId ?? 1,
                LocationId = 1, // Pass LocationId (FK) instead of assigning a string to Location
                OrganizationId = 1, // Required non-nullable FK
                UserId = 1, // Required non-nullable FK
                NumberOfFloors = 1,
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Building '{building.Name}' created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var building = await _context.Buildings
                .Include(b => b.Location)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (building == null) return NotFound();

            var model = new BuildingFormViewModel
            {
                Id = building.Id,
                Name = building.Name ?? string.Empty,
                BuildingTypeId = building.BuildingTypeId,
                Location = building.Location?.Name ?? string.Empty, // Read Name string from Location entity
                CityId = building.CityId,
                UseTypeId = building.UseTypeId,
                IsActive = building.IsActive
            };

            ViewBag.BuildingTypes = new SelectList(await _context.BuildingTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingTypeId);
            return View("CreateEdit", model);
        }

        // POST: Buildings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BuildingFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.BuildingTypes = new SelectList(await _context.BuildingTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingTypeId);
                return View("CreateEdit", model);
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null || building.IsDeleted) return NotFound();

            building.Name = model.Name;
            building.BuildingTypeId = model.BuildingTypeId;
            building.IsActive = model.IsActive;

            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Building '{building.Name}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var building = await _context.Buildings
                .Include(b => b.BuildingType)
                .Include(b => b.Floors).ThenInclude(f => f.Rooms).ThenInclude(r => r.RoomStatus)
                .Include(b => b.BuildingImages)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (building == null) return NotFound();

            var floors = building.Floors.Where(f => !f.IsDeleted).ToList();
            var rooms = floors.SelectMany(f => f.Rooms).Where(r => !r.IsDeleted).ToList();
            var images = building.BuildingImages.Where(i => !i.IsDeleted).ToList();
            var specs = await _context.BuildingSpecifications.Where(s => !s.IsDeleted).ToListAsync();

            var viewModel = new BuildingDetailsViewModel
            {
                Building = building,
                Floors = floors,
                Rooms = rooms,
                Shops = new System.Collections.Generic.List<Shop>(),
                Images = images,
                Specifications = specs
            };

            return View(viewModel);
        }

        // GET: Buildings/Images/5
        public async Task<IActionResult> Images(int id)
        {
            var building = await _context.Buildings
                .Include(b => b.BuildingImages)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

            if (building == null) return NotFound();

            var viewModel = new BuildingImageUploadViewModel
            {
                BuildingId = building.Id,
                BuildingName = building.Name ?? string.Empty,
                ExistingImages = building.BuildingImages.Where(i => !i.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        // POST: Buildings/UploadImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(BuildingImageUploadViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Images), new { id = model.BuildingId });

            var image = new BuildingImage
            {
                BuildingId = model.BuildingId,
                Url = model.ImageUrl,
                IsActive = true,
                IsDeleted = false
            };

            _context.BuildingImages.Add(image);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Image added successfully!";
            return RedirectToAction(nameof(Images), new { id = model.BuildingId });
        }

        // GET: Buildings/Specifications/5
        public async Task<IActionResult> Specifications(int id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (building == null) return NotFound();

            var specs = await _context.BuildingSpecifications.Where(s => !s.IsDeleted).ToListAsync();

            var viewModel = new BuildingSpecViewModel
            {
                BuildingId = building.Id,
                BuildingName = building.Name ?? string.Empty,
                Specifications = specs
            };

            return View(viewModel);
        }

        // POST: Buildings/SaveSpecification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSpecification(BuildingSpecViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                TempData["ErrorMessage"] = "Title is required.";
                return RedirectToAction(nameof(Specifications), new { id = model.BuildingId });
            }

            var spec = new BuildingSpecification
            {
                Name = model.Title,
                UseTypeId = 1,
                NumberOfFloor = 1,
                CityId = 1,
                LocationId = 1,
                BuildingRequestId = 1,
                IsActive = true,
                IsDeleted = false
            };

            _context.BuildingSpecifications.Add(spec);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Building specification added successfully!";
            return RedirectToAction(nameof(Specifications), new { id = model.BuildingId });
        }

        // GET: Buildings/Types
        public async Task<IActionResult> Types()
        {
            var types = await _context.BuildingTypes.Where(t => !t.IsDeleted).ToListAsync();
            var viewModel = new BuildingTypeFormViewModel { ExistingTypes = types };
            return View(viewModel);
        }

        // POST: Buildings/CreateType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateType(BuildingTypeFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                TempData["ErrorMessage"] = "Building Type Name is required.";
                return RedirectToAction(nameof(Types));
            }

            var buildingType = new BuildingType
            {
                Name = model.Name,
                IsActive = true,
                IsDeleted = false
            };

            _context.BuildingTypes.Add(buildingType);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Building Type '{model.Name}' created!";
            return RedirectToAction(nameof(Types));
        }
    }
}