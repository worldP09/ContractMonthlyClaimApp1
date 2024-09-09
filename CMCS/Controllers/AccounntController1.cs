// Controllers/AccountController.cs
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims; // Use this for authentication claims
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class AccountController : Controller
    {
        private readonly CMCSContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(CMCSContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check user in all tables
                var lecturer = _context.Lecturers
                                       .FirstOrDefault(l => l.Email == model.Email && l.Password == model.Password);

                var academicManager = _context.AcademicManagers
                                              .FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

                var coordinator = _context.Coordinators
                                          .FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);

                if (lecturer != null)
                {
                    // Lecturer login
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, lecturer.Email),
                        new System.Security.Claims.Claim(ClaimTypes.Role, "Lecturer")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Dashboard", "Lecturer", new { lecturerId = lecturer.LecturerID });
                }
                else if (academicManager != null)
                {
                    // Academic Manager login
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, academicManager.Email),
                        new System.Security.Claims.Claim(ClaimTypes.Role, "AcademicManager")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Dashboard", "AcademicManager", new { managerId = academicManager.ManagerID });
                }
                else if (coordinator != null)
                {
                    // Coordinator login
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, coordinator.Email),
                        new System.Security.Claims.Claim(ClaimTypes.Role, "Coordinator")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Dashboard", "Coordinator", new { coordinatorId = coordinator.CoordinatorID });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }
    }
}
