using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

public class LecturerController : Controller
{
    private readonly CMCSContext _context;
    private readonly ILogger<LecturerController> _logger;

    public LecturerController(CMCSContext context, ILogger<LecturerController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Dashboard
    public IActionResult Dashboard(int lecturerId)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerID == lecturerId);
        if (lecturer == null)
        {
            _logger.LogWarning("Lecturer not found: {LecturerID}", lecturerId);
            return NotFound();
        }
        return View(lecturer);
    }

    // GET: Submit Claim
    public IActionResult SubmitClaim()
    {
        var claim = new Claim(); // Use the alias for CMCS.Models.Claim
        return View(claim);
    }

    // POST: Submit Claim
    [HttpPost]
    public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile SupportingDocuments)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerID == claim.LecturerID);
        if (lecturer == null)
        {
            ModelState.AddModelError(string.Empty, "Lecturer not found.");
            return View(claim);
        }

        if (ModelState.IsValid)
        {
            // Handle file upload
            if (SupportingDocuments != null && SupportingDocuments.Length > 0)
            {
                var safeFileName = Path.GetFileNameWithoutExtension(SupportingDocuments.FileName)
                                    .Replace(" ", "_")
                                    + Path.GetExtension(SupportingDocuments.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "claims", safeFileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await SupportingDocuments.CopyToAsync(stream);
                }

                claim.SupportingDocumentPath = $"/uploads/claims/{safeFileName}";
            }

            claim.DateSubmitted = DateTime.Now;
            claim.Status = "Pending";
            claim.TotalClaimAmount = claim.HoursWorked * lecturer.HourlyRate;
            _context.Claims.Add(claim);
            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim submitted successfully.";
                return RedirectToAction("Dashboard", new { lecturerId = claim.LecturerID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting claim.");
                ModelState.AddModelError(string.Empty, "An error occurred while submitting your claim. Please try again.");
            }
        }
        return View(claim);
    }

    // GET: Track Claims
    public IActionResult TrackClaims()
    {
        var lecturerEmail = User.Identity.Name;
        var lecturer = _context.Lecturers.FirstOrDefault(l => l.Email == lecturerEmail);

        if (lecturer == null)
        {
            _logger.LogWarning("Lecturer not found for email: {Email}", lecturerEmail);
            return NotFound();
        }

        var claims = _context.Claims.Where(c => c.LecturerID == lecturer.LecturerID).ToList();

        if (claims.Count == 0)
        {
            _logger.LogInformation("No claims found for lecturer ID: {LecturerID}", lecturer.LecturerID);
        }

        return View(claims);
    }
}
