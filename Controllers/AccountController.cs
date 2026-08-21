using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BMS.Models;
using BMS.Models.Auth;
using BMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AccountController(DBContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (!ModelState.IsValid)
                return View(model);

            // 1. Locate user via User.UserName, User.Email, or linked UserEmails record
            var user = await _context.Users
                .Include(u => u.UserEmails)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    (u.UserName == model.Identity ||
                     u.Email == model.Identity ||
                     u.UserEmails.Any(e => e.Email == model.Identity && e.IsActive && !e.IsDeleted))
                    && u.IsActive && !u.IsDeleted);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login credentials.");
                return View(model);
            }

            // 2. Check account block status
            if (user.BlockEndDate.HasValue && user.BlockEndDate.Value > DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, $"Account locked until {user.BlockEndDate.Value.ToLocalTime():g}.");
                return View(model);
            }

            // 3. Verify Password
            bool isValidPassword = _passwordHasher.VerifyPassword(user.Password, model.Password);

            // Extract client metadata for UserLogon audit record
            var userAgentStr = Request.Headers.UserAgent.ToString();
            var (browser, platform) = ParseUserAgent(userAgentStr);

            if (!isValidPassword)
            {
                user.FailureCount = (user.FailureCount ?? 0) + 1;

                // Lock account for 15 minutes after 5 failed attempts
                if (user.FailureCount >= 5)
                {
                    user.BlockEndDate = DateTime.UtcNow.AddMinutes(15);
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                ModelState.AddModelError(string.Empty, "Invalid login credentials.");
                return View(model);
            }

            // 4. Reset failure count & update LastLogon timestamp
            user.FailureCount = 0;
            user.BlockEndDate = null;
            user.LastLogon = DateTime.UtcNow;

            // 5. Audit successful authentication attempt in UserLogon
            var logonAudit = new UserLogon
            {
                UserId = user.Id,
                FingerPrint = string.IsNullOrWhiteSpace(model.FingerPrint) ? "UNKNOWN" : model.FingerPrint,
                UserAgent = userAgentStr.Length > 250 ? userAgentStr[..250] : userAgentStr,
                Platform = platform,
                Browser = browser,
                TimeZone = string.IsNullOrWhiteSpace(model.TimeZone) ? "UTC" : model.TimeZone,
                LogDate = DateTime.UtcNow,
                VerificationCode = "N/A",
                IsVerified = true,
                IsActive = true,
                IsDeleted = false
            };

            _context.UserLogons.Add(logonAudit);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // 6. Build Claims identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim("FullName", user.FullName ?? string.Empty)
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            foreach (var userRole in user.UserRoles.Where(r => r.IsActive && !r.IsDeleted))
            {
                if (userRole.Role?.Name != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = model.RememberMe });

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        private static (string Browser, string Platform) ParseUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return ("Unknown", "Unknown");

            string platform = userAgent.Contains("Windows") ? "Windows" :
                              userAgent.Contains("Macintosh") ? "macOS" :
                              userAgent.Contains("Linux") ? "Linux" :
                              userAgent.Contains("Android") ? "Android" :
                              userAgent.Contains("iPhone") ? "iOS" : "Other";

            string browser = userAgent.Contains("Edg") ? "Edge" :
                             userAgent.Contains("Chrome") ? "Chrome" :
                             userAgent.Contains("Firefox") ? "Firefox" :
                             userAgent.Contains("Safari") ? "Safari" : "Other";

            return (browser, platform);
        }
    }
}