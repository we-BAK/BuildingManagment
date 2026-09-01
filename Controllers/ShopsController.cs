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
    public class ShopsController : Controller
    {
        private readonly DBContext _context;

        public ShopsController(DBContext context)
        {
            _context = context;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
            var shops = await _context.Shops
                .Include(s => s.User)
                .Include(s => s.BusinessArea)
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.Id)
                .ToListAsync();

            return View(shops);
        }

        // GET: Shops/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.BusinessAreas = new SelectList(await _context.BusinessAreas.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            return View("CreateEdit", new ShopFormViewModel());
        }

        // POST: Shops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name", model.BuildingId);
                ViewBag.BusinessAreas = new SelectList(await _context.BusinessAreas.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
                return View("CreateEdit", model);
            }

            var shop = new Shop
            {
                Name = model.ShopNumber,
                UserId = 1,
                BusinessAreaId = 1,
                Description = model.Description ?? "Commercial Retail Space",
                CreatedDate = DateTime.UtcNow,
                IsActive = model.IsActive,
                IsDeleted = false
            };

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Shop '{shop.Name}' created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null || shop.IsDeleted) return NotFound();

            var model = new ShopFormViewModel
            {
                Id = shop.Id,
                BuildingId = 1,
                ShopNumber = shop.Name ?? string.Empty,
                Size = 1000,
                FacadeType = "Glass Frontage",
                Description = shop.Description ?? string.Empty,
                IsActive = shop.IsActive
            };

            ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.BusinessAreas = new SelectList(await _context.BusinessAreas.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
            return View("CreateEdit", model);
        }

        // POST: Shops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ShopFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Buildings = new SelectList(await _context.Buildings.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
                ViewBag.BusinessAreas = new SelectList(await _context.BusinessAreas.Where(b => !b.IsDeleted).ToListAsync(), "Id", "Name");
                return View("CreateEdit", model);
            }

            var shop = await _context.Shops.FindAsync(id);
            if (shop == null || shop.IsDeleted) return NotFound();

            shop.Name = model.ShopNumber;
            if (!string.IsNullOrWhiteSpace(model.Description)) shop.Description = model.Description;
            shop.IsActive = model.IsActive;

            _context.Shops.Update(shop);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Shop '{shop.Name}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var shop = await _context.Shops
                .Include(s => s.User)
                .Include(s => s.BusinessArea)
                .Include(s => s.ShopImages)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (shop == null) return NotFound();

            var viewModel = new ShopDetailsViewModel
            {
                Shop = shop,
                Images = shop.ShopImages.Where(i => !i.IsDeleted).ToList(),
                Specifications = new System.Collections.Generic.List<ShopSpecification>(),
                RentalRequests = new System.Collections.Generic.List<ShopRequest>()
            };

            return View(viewModel);
        }

        // GET: Shops/Images/5
        public async Task<IActionResult> Images(int id)
        {
            var shop = await _context.Shops
                .Include(s => s.ShopImages)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (shop == null) return NotFound();

            var viewModel = new ShopImageViewModel
            {
                ShopId = shop.Id,
                ShopNumber = shop.Name ?? string.Empty,
                ExistingImages = shop.ShopImages.Where(i => !i.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        // POST: Shops/UploadImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(ShopImageViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                TempData["ErrorMessage"] = "Image URL is required.";
                return RedirectToAction(nameof(Images), new { id = model.ShopId });
            }

            var shopImage = new ShopImage
            {
                ShopId = model.ShopId,
                ImageUrl = model.ImageUrl, // Fixed: Changed 'Photo' to 'ImageUrl'
                Description = model.Description ?? "Shop Image", // Required field in ShopImage
                IsActive = true,
                IsDeleted = false
            };

            _context.ShopImages.Add(shopImage);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Shop storefront image uploaded!";
            return RedirectToAction(nameof(Images), new { id = model.ShopId });
        }

        // GET: Shops/Specifications/5
        public async Task<IActionResult> Specifications(int id)
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            if (shop == null) return NotFound();

            var specs = await _context.ShopSpecifications.Where(sp => !sp.IsDeleted).ToListAsync();

            var viewModel = new ShopSpecViewModel
            {
                ShopId = shop.Id,
                ShopNumber = shop.Name ?? string.Empty,
                ExistingSpecifications = specs
            };

            return View(viewModel);
        }

        // POST: Shops/SaveSpecification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSpecification(ShopSpecViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FeatureName))
            {
                TempData["ErrorMessage"] = "Attribute title is required.";
                return RedirectToAction(nameof(Specifications), new { id = model.ShopId });
            }

            var spec = new ShopSpecification
            {
                Name = model.FeatureName,
                ShopRequestId = 1,
                UseTypeId = 1,
                IsActive = true,
                IsDeleted = false
            };

            _context.ShopSpecifications.Add(spec);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Shop technical specification added!";
            return RedirectToAction(nameof(Specifications), new { id = model.ShopId });
        }
    }
}
