//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using BMS.Models;
//using BMS.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BMS.Controllers
//{
//    public class RentalRequestsController : Controller
//    {
//        private readonly DBContext _context;

//        public RentalRequestsController(DBContext context)
//        {
//            _context = context;
//        }

//        // GET: RentalRequests
//        public async Task<IActionResult> Index()
//        {
//            var roomRequests = await _context.RoomRentalRequests
//                .Include(r => r.Room).ThenInclude(rm => rm.Building)
//                .Include(r => r.User)
//                .Include(r => r.Status)
//                .Where(r => !r.IsDeleted)
//                .OrderByDescending(r => r.CreatedDate)
//                .ToListAsync();

//            return View(roomRequests);
//        }

//        // GET: RentalRequests/ShopRequests
//        public async Task<IActionResult> ShopRequests()
//        {
//            var shopRequests = await _context.ShopRequests
//                .Include(s => s.Shop).ThenInclude(sp => sp.Building)
//                .Include(s => s.User)
//                .Include(s => s.Status)
//                .Where(s => !s.IsDeleted)
//                .OrderByDescending(s => s.CreatedDate)
//                .ToListAsync();

//            return View(shopRequests);
//        }

//        // GET: RentalRequests/Review/5?type=Room
//        public async Task<IActionResult> Review(int id, string type = "Room")
//        {
//            if (type == "Shop")
//            {
//                var shopReq = await _context.ShopRequests
//                    .Include(s => s.Shop).ThenInclude(sp => sp.Building)
//                    .Include(s => s.User)
//                    .Include(s => s.Status)
//                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

//                if (shopReq == null) return NotFound();

//                var viewModel = new RentalRequestReviewViewModel
//                {
//                    RequestId = shopReq.Id,
//                    RequestType = "Shop",
//                    ApplicantName = shopReq.User?.FullName ?? "N/A",
//                    ApplicantEmail = shopReq.User?.Email ?? "N/A",
//                    ApplicantPhone = shopReq.User?.PhoneNumber ?? "N/A",
//                    PropertyName = $"Shop {shopReq.Shop?.ShopNumber} ({shopReq.Shop?.Building?.Name})",
//                    RequestDate = shopReq.CreatedDate,
//                    CurrentStatus = shopReq.Status?.Name ?? "Pending",
//                    Notes = shopReq.Remarks ?? "N/A"
//                };
//                return View(viewModel);
//            }

//            var roomReq = await _context.RoomRentalRequests
//                .Include(r => r.Room).ThenInclude(rm => rm.Building)
//                .Include(r => r.User)
//                .Include(r => r.Status)
//                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

//            if (roomReq == null) return NotFound();

//            var roomViewModel = new RentalRequestReviewViewModel
//            {
//                RequestId = roomReq.Id,
//                RequestType = "Room",
//                ApplicantName = roomReq.User?.FullName ?? "N/A",
//                ApplicantEmail = roomReq.User?.Email ?? "N/A",
//                ApplicantPhone = roomReq.User?.PhoneNumber ?? "N/A",
//                PropertyName = $"Room {roomReq.Room?.RoomNumber} ({roomReq.Room?.Building?.Name})",
//                RequestDate = roomReq.CreatedDate,
//                CurrentStatus = roomReq.Status?.Name ?? "Pending",
//                Notes = roomReq.Remarks ?? "N/A"
//            };

//            return View(roomViewModel);
//        }

//        // POST: RentalRequests/Approve
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Approve(RentalRequestReviewViewModel model)
//        {
//            if (model.RequestType == "Shop")
//            {
//                var shopReq = await _context.ShopRequests.FindAsync(model.RequestId);
//                if (shopReq != null)
//                {
//                    shopReq.StatusId = 2; // Approved
//                    shopReq.Remarks = (shopReq.Remarks + " | Staff Approved: " + model.ReviewNotes).Trim();
//                    _context.ShopRequests.Update(shopReq);
//                    await _context.SaveChangesAsync();
//                }
//                TempData["SuccessMessage"] = "Shop rental request approved!";
//                return RedirectToAction(nameof(ShopRequests));
//            }

//            var roomReq = await _context.RoomRentalRequests.FindAsync(model.RequestId);
//            if (roomReq != null)
//            {
//                roomReq.StatusId = 2; // Approved
//                roomReq.Remarks = (roomReq.Remarks + " | Staff Approved: " + model.ReviewNotes).Trim();
//                _context.RoomRentalRequests.Update(roomReq);
//                await _context.SaveChangesAsync();
//            }

//            TempData["SuccessMessage"] = "Room rental request approved!";
//            return RedirectToAction(nameof(Index));
//        }

//        // POST: RentalRequests/Decline
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Decline(RentalRequestReviewViewModel model)
//        {
//            if (model.RequestType == "Shop")
//            {
//                var shopReq = await _context.ShopRequests.FindAsync(model.RequestId);
//                if (shopReq != null)
//                {
//                    shopReq.StatusId = 3; // Declined
//                    shopReq.Remarks = (shopReq.Remarks + " | Staff Declined: " + model.ReviewNotes).Trim();
//                    _context.ShopRequests.Update(shopReq);
//                    await _context.SaveChangesAsync();
//                }
//                TempData["ErrorMessage"] = "Shop rental request declined.";
//                return RedirectToAction(nameof(ShopRequests));
//            }

//            var roomReq = await _context.RoomRentalRequests.FindAsync(model.RequestId);
//            if (roomReq != null)
//            {
//                roomReq.StatusId = 3; // Declined
//                roomReq.Remarks = (roomReq.Remarks + " | Staff Declined: " + model.ReviewNotes).Trim();
//                _context.RoomRentalRequests.Update(roomReq);
//                await _context.SaveChangesAsync();
//            }

//            TempData["ErrorMessage"] = "Room rental request declined.";
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
