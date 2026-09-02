using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly DBContext _context;

        public InventoryController(DBContext context)
        {
            _context = context;
        }

        // GET: Inventory/ShopItems
        public async Task<IActionResult> ShopItems()
        {
            var shopItems = await _context.ShopItems
                .Include(si => si.Item)
                .Include(si => si.Shop)
                .Where(si => !si.IsDeleted)
                .OrderByDescending(si => si.Id)
                .ToListAsync();

            return View(shopItems);
        }

        // GET: Inventory/MaintenanceItems
        public async Task<IActionResult> MaintenanceItems()
        {
            // For maintenance items, we will retrieve items in specific categories 
            // (or all items that can be assigned to maintenance)
            var maintenanceItems = await _context.Items
                .Include(i => i.ItemCategory)
                .Where(i => !i.IsDeleted)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            return View(maintenanceItems);
        }
    }
}
