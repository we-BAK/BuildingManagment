using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContext _context;

        public AdminController(DBContext context)
        {
            _context = context;
        }

        // GET: Admin/Organizations
        public async Task<IActionResult> Organizations()
        {
            var orgs = await _context.Organizations
                .Include(o => o.OrganizationType)
                .Where(o => !o.IsDeleted)
                .OrderBy(o => o.Name)
                .ToListAsync();

            return View(orgs);
        }

        // GET: Admin/Locations
        public async Task<IActionResult> Locations()
        {
            var locations = await _context.Locations
                .Include(l => l.City)
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.Name)
                .ToListAsync();

            return View(locations);
        }

        // GET: Admin/Cities
        public async Task<IActionResult> Cities()
        {
            var cities = await _context.Cities
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(cities);
        }

        // GET: Admin/BusinessAreas
        public async Task<IActionResult> BusinessAreas()
        {
            var areas = await _context.BusinessAreas
                .Where(b => !b.IsDeleted)
                .OrderBy(b => b.Name)
                .ToListAsync();

            return View(areas);
        }
    }
}
