using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using BMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class RentalRequestsController : Controller
    {
        private readonly DBContext _context;

        public RentalRequestsController(DBContext context)
        {
            _context = context;
        }

        // GET: RentalRequests
        public async Task<IActionResult> Index()
        {
            var roomRequests = await _context.RoomRentalRequests
                .Include(r => r.Room).ThenInclude(rm => rm.Floor).ThenInclude(f => f.Building)
                .Include(r => r.User)
                .Include(r => r.RequestStatus)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.RequestedDate)
                .ToListAsync();

            return View(roomRequests);
        }

        // GET: RentalRequests/ShopRequests
        public async Task<IActionResult> ShopRequests()
        {
            var shopRequests = await _context.ShopRequests
                .Include(s => s.User)
                .Include(s => s.RequestStatus)
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.RequestDate)
                .ToListAsync();

            return View(shopRequests);
        }

        // GET: RentalRequests/Review/5?type=Room
        public async Task<IActionResult> Review(int id, string type = "Room")
        {
            if (type == "Shop")
            {
                var shopReq = await _context.ShopRequests
                    .Include(s => s.User)
                    .Include(s => s.RequestStatus)
                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

                if (shopReq == null) return NotFound();

                var viewModel = new RentalRequestReviewViewModel
                {
                    RequestId = shopReq.Id,
                    RequestType = "Shop",
                    ApplicantName = shopReq.User?.FullName ?? "N/A",
                    ApplicantEmail = shopReq.User?.Email ?? "N/A",
                    ApplicantPhone = shopReq.User?.PhoneNumber ?? "N/A",
                    PropertyName = $"{shopReq.NumberOfShops} Shop(s) Requested",
                    RequestDate = shopReq.RequestDate,
                    CurrentStatus = shopReq.RequestStatus?.Name ?? "Pending",
                    Notes = shopReq.Description ?? "N/A"
                };
                return View(viewModel);
            }

            var roomReq = await _context.RoomRentalRequests
                .Include(r => r.Room).ThenInclude(rm => rm.Floor).ThenInclude(f => f.Building)
                .Include(r => r.User)
                .Include(r => r.RequestStatus)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (roomReq == null) return NotFound();

            var roomViewModel = new RentalRequestReviewViewModel
            {
                RequestId = roomReq.Id,
                RequestType = "Room",
                ApplicantName = roomReq.User?.FullName ?? "N/A",
                ApplicantEmail = roomReq.User?.Email ?? "N/A",
                ApplicantPhone = roomReq.User?.PhoneNumber ?? "N/A",
                PropertyName = $"Room {roomReq.Room?.Name} ({roomReq.Room?.Floor?.Building?.Name})",
                RequestDate = roomReq.RequestedDate,
                CurrentStatus = roomReq.RequestStatus?.Name ?? "Pending",
                Notes = roomReq.Description ?? "N/A"
            };

            return View(roomViewModel);
        }

        // POST: RentalRequests/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(RentalRequestReviewViewModel model)
        {
            if (model.RequestType == "Shop")
            {
                var shopReq = await _context.ShopRequests.FindAsync(model.RequestId);
                if (shopReq != null)
                {
                    shopReq.RequestStatusId = 2; // Approved
                    shopReq.Description = (shopReq.Description + " | Staff Approved: " + model.ReviewNotes).Trim();
                    _context.ShopRequests.Update(shopReq);
                    await _context.SaveChangesAsync();
                }
                TempData["SuccessMessage"] = "Shop rental request approved!";
                return RedirectToAction(nameof(ShopRequests));
            }

            var roomReq = await _context.RoomRentalRequests.FindAsync(model.RequestId);
            if (roomReq != null)
            {
                roomReq.RequestStatusId = 2; // Approved
                roomReq.Description = (roomReq.Description + " | Staff Approved: " + model.ReviewNotes).Trim();
                _context.RoomRentalRequests.Update(roomReq);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Room rental request approved!";
            return RedirectToAction(nameof(Index));
        }

        // POST: RentalRequests/Decline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(RentalRequestReviewViewModel model)
        {
            if (model.RequestType == "Shop")
            {
                var shopReq = await _context.ShopRequests.FindAsync(model.RequestId);
                if (shopReq != null)
                {
                    shopReq.RequestStatusId = 3; // Declined
                    shopReq.Description = (shopReq.Description + " | Staff Declined: " + model.ReviewNotes).Trim();
                    _context.ShopRequests.Update(shopReq);
                    await _context.SaveChangesAsync();
                }
                TempData["ErrorMessage"] = "Shop rental request declined.";
                return RedirectToAction(nameof(ShopRequests));
            }

            var roomReq = await _context.RoomRentalRequests.FindAsync(model.RequestId);
            if (roomReq != null)
            {
                roomReq.RequestStatusId = 3; // Declined
                roomReq.Description = (roomReq.Description + " | Staff Declined: " + model.ReviewNotes).Trim();
                _context.RoomRentalRequests.Update(roomReq);
                await _context.SaveChangesAsync();
            }

            TempData["ErrorMessage"] = "Room rental request declined.";
            return RedirectToAction(nameof(Index));
        }
    }
}