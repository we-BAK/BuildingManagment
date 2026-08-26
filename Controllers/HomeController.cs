using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using BMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(DBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var totalBuildings = await _context.Buildings.CountAsync(b => !b.IsDeleted);
            var totalRooms = await _context.Rooms.CountAsync(r => !r.IsDeleted);
            var totalShops = await _context.Shops.CountAsync(s => !s.IsDeleted);
            var activeRentalsCount = await _context.RoomRentals.CountAsync(rr => rr.IsActive && !rr.IsDeleted);

            var pendingRoomRequestsCount = await _context.RoomRentalRequests.CountAsync(r => !r.IsDeleted);
            var pendingShopRequestsCount = await _context.ShopRequests.CountAsync(s => !s.IsDeleted);

            int totalUnits = totalRooms + totalShops;
            decimal occupancyRate = totalUnits > 0 ? Math.Round((decimal)activeRentalsCount / totalUnits * 100, 1) : 0;

            var totalMonthlyRevenue = await _context.RoomRentals
                .Where(r => r.IsActive && !r.IsDeleted)
                .SumAsync(r => (decimal)r.TotalPrice);

            var pendingRoomRequests = await _context.RoomRentalRequests
                .Include(r => r.Room).ThenInclude(rm => rm.Floor).ThenInclude(f => f.Building)
                .Include(r => r.User)
                .Include(r => r.RequestStatus)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.RequestedDate)
                .Take(5)
                .ToListAsync();

            var pendingShopRequests = await _context.ShopRequests
                .Include(s => s.User)
                .Include(s => s.RequestStatus)
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.RequestDate)
                .Take(5)
                .ToListAsync();

            var upcomingExpirations = await _context.RoomRentals
                .Include(rr => rr.Room).ThenInclude(rm => rm.Floor).ThenInclude(f => f.Building)
                .Include(rr => rr.Tenant)
                .Where(rr => rr.IsActive && !rr.IsDeleted)
                .OrderByDescending(rr => rr.StartDate)
                .Take(5)
                .ToListAsync();

            // Updated line 67: Uses NotificationDate property from Notification model
            var recentNotifications = await _context.Notifications
                .Where(n => !n.IsDeleted)
                .OrderByDescending(n => n.NotificationDate)
                .Take(5)
                .ToListAsync();

            var recentBuildings = await _context.Buildings
                .Include(b => b.BuildingType)
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.Id)
                .Take(5)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalBuildings = totalBuildings,
                TotalRooms = totalRooms,
                TotalShops = totalShops,
                ActiveRentalsCount = activeRentalsCount,
                PendingRequestsCount = pendingRoomRequestsCount + pendingShopRequestsCount,
                OccupancyRate = occupancyRate,
                TotalMonthlyRevenue = totalMonthlyRevenue,
                PendingRoomRequests = pendingRoomRequests,
                PendingShopRequests = pendingShopRequests,
                UpcomingExpirations = upcomingExpirations,
                RecentNotifications = recentNotifications,
                RecentBuildings = recentBuildings
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}