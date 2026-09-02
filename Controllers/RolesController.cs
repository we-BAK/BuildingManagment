using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class RolesController : Controller
    {
        private readonly DBContext _context;

        public RolesController(DBContext context)
        {
            _context = context;
        }

        // GET: Roles/Index
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Name)
                .ToListAsync();

            return View(roles);
        }
    }
}
