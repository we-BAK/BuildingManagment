using System;
using System.Linq;
using System.Threading.Tasks;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly DBContext _context;

        public UsersController(DBContext context)
        {
            _context = context;
        }

        // GET: Users/Index
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => !u.IsDeleted)
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return View(users);
        }

        // GET: Users/CreateEdit/5
        public async Task<IActionResult> CreateEdit(int? id)
        {
            ViewBag.Roles = new SelectList(await _context.Roles.Where(r => !r.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.Sexes = new SelectList(await _context.Sexes.ToListAsync(), "Id", "Name");

            if (id == null)
            {
                return View(new User());
            }

            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(int id, User user, int[] SelectedRoles)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0) // Create
                    {
                        user.IsActive = true;
                        user.IsDeleted = false;
                        user.CreatedDate = DateTime.UtcNow;
                        user.Password = "DefaultHash"; // In real scenario, use IPasswordHasher
                        user.DefaultLanguageId = 1;
                        
                        _context.Users.Add(user);
                        await _context.SaveChangesAsync();

                        // Add roles
                        if (SelectedRoles != null)
                        {
                            foreach (var roleId in SelectedRoles)
                            {
                                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId, IsActive = true, IsDefault = false });
                            }
                            await _context.SaveChangesAsync();
                        }
                        TempData["SuccessMessage"] = "User created successfully!";
                    }
                    else // Edit
                    {
                        var existingUser = await _context.Users
                            .Include(u => u.UserRoles)
                            .FirstOrDefaultAsync(u => u.Id == id);
                            
                        if (existingUser != null)
                        {
                            existingUser.FirstName = user.FirstName;
                            existingUser.LastName = user.LastName;
                            existingUser.FullName = user.FullName;
                            existingUser.Email = user.Email;
                            existingUser.UserName = user.UserName;
                            existingUser.PhoneNumber = user.PhoneNumber;
                            existingUser.IsActive = user.IsActive;

                            // Update roles
                            _context.UserRoles.RemoveRange(existingUser.UserRoles);
                            
                            if (SelectedRoles != null)
                            {
                                foreach (var roleId in SelectedRoles)
                                {
                                    _context.UserRoles.Add(new UserRole { UserId = existingUser.Id, RoleId = roleId, IsActive = true, IsDefault = false });
                                }
                            }
                            
                            _context.Users.Update(existingUser);
                            await _context.SaveChangesAsync();
                            TempData["SuccessMessage"] = "User updated successfully!";
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error saving user.");
                }
            }

            ViewBag.Roles = new SelectList(await _context.Roles.Where(r => !r.IsDeleted).ToListAsync(), "Id", "Name");
            ViewBag.Sexes = new SelectList(await _context.Sexes.ToListAsync(), "Id", "Name", user.SexId);
            return View(user);
        }

        // GET: Users/Logons
        public async Task<IActionResult> Logons()
        {
            var logons = await _context.UserLogons
                .Include(l => l.User)
                .OrderByDescending(l => l.LogDate)
                .Take(500)
                .ToListAsync();

            return View(logons);
        }
    }
}
