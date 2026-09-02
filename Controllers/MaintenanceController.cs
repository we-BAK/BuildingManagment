using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMS.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly DBContext _context;

        public MaintenanceController(DBContext context)
        {
            _context = context;
        }

        // GET: Maintenance/Index
        public async Task<IActionResult> Index()
        {
            var requests = await _context.MaintenanceRequests
                .Include(r => r.User)
                .Include(r => r.Room)
                .Include(r => r.MaintenanceType)
                .Include(r => r.MaintenanceStatus)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.DateSubmitted)
                .ToListAsync();

            return View(requests);
        }

        // GET: Maintenance/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = new SelectList(await _context.Users.Where(u => !u.IsDeleted).ToListAsync(), "Id", "FullName");
            ViewBag.Rooms = new SelectList(await _context.Rooms.Where(r => !r.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.MaintenanceTypes = new SelectList(await _context.MaintenanceTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Maintenance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                var defaultStatus = await _context.MaintenanceStatuses.FirstOrDefaultAsync(s => s.Name == "Submitted");
                request.MaintenanceStatusId = defaultStatus?.Id ?? 1; // Fallback to 1 if not found
                request.DateSubmitted = DateOnly.FromDateTime(DateTime.Today);
                request.IsActive = true;
                request.IsDeleted = false;

                _context.MaintenanceRequests.Add(request);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Maintenance request submitted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = new SelectList(await _context.Users.Where(u => !u.IsDeleted).ToListAsync(), "Id", "FullName", request.UserId);
            ViewBag.Rooms = new SelectList(await _context.Rooms.Where(r => !r.IsDeleted).ToListAsync(), "Id", "Name", request.RoomId);
            ViewBag.MaintenanceTypes = new SelectList(await _context.MaintenanceTypes.Where(t => !t.IsDeleted).ToListAsync(), "Id", "Name", request.MaintenanceTypeId);
            return View(request);
        }

        // GET: Maintenance/Allocations
        public async Task<IActionResult> Allocations()
        {
            var allocations = await _context.MaintenanceRequestAllocations
                .Include(a => a.MaintenanceRequest)
                    .ThenInclude(r => r.Room)
                .Include(a => a.BuildingEmployee)
                    .ThenInclude(be => be.Employee)
                .Include(a => a.AllocationStatus)
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.AllocatedDate)
                .ToListAsync();

            return View(allocations);
        }

        // GET: Maintenance/Types
        public async Task<IActionResult> Types()
        {
            var types = await _context.MaintenanceTypes.Where(t => !t.IsDeleted).ToListAsync();
            return View(types);
        }

        // GET: Maintenance/Statuses
        public async Task<IActionResult> Statuses()
        {
            var statuses = await _context.MaintenanceStatuses.Where(s => !s.IsDeleted).ToListAsync();
            return View(statuses);
        }

        // GET: Maintenance/Reports
        public async Task<IActionResult> Reports()
        {
            // Simple report data
            var totalRequests = await _context.MaintenanceRequests.CountAsync(r => !r.IsDeleted);
            
            var completedStatusIds = await _context.MaintenanceStatuses
                .Where(s => s.Name.Contains("Complete") || s.Name.Contains("Done"))
                .Select(s => s.Id)
                .ToListAsync();

            var completedRequests = await _context.MaintenanceRequests
                .CountAsync(r => !r.IsDeleted && completedStatusIds.Contains(r.MaintenanceStatusId));
            
            var pendingRequests = totalRequests - completedRequests;

            var typesBreakdown = await _context.MaintenanceRequests
                .Where(r => !r.IsDeleted)
                .GroupBy(r => r.MaintenanceType.Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.TotalRequests = totalRequests;
            ViewBag.CompletedRequests = completedRequests;
            ViewBag.PendingRequests = pendingRequests;
            ViewBag.TypesBreakdown = typesBreakdown.ToDictionary(x => x.Type, x => x.Count);

            return View();
        }
    }
}
