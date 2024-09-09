using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AcademicManagerController : Controller
{
    private readonly CMCSContext _context;

    public AcademicManagerController(CMCSContext context)
    {
        _context = context;
    }

    // GET: View All Claims Assigned to Academic Manager
    public IActionResult ViewClaims(int managerId)
    {
        var manager = _context.AcademicManagers
            .Where(m => m.ManagerID == managerId)
            .FirstOrDefault();

        if (manager == null)
        {
            return NotFound();
        }

        var claims = _context.Claims
            .Where(c => c.ManagerID == managerId)
            .ToList();

        return View(claims);
    }

    // POST: Approve Claim
    [HttpPost]
    public IActionResult ApproveClaim(int claimId)
    {
        var claim = _context.Claims.Find(claimId);
        if (claim == null)
        {
            return NotFound();
        }

        claim.Status = "Approved";
        _context.SaveChanges();

        return RedirectToAction("ViewClaims", new { managerId = claim.ManagerID });
    }

    // POST: Reject Claim
    [HttpPost]
    public IActionResult RejectClaim(int claimId)
    {
        var claim = _context.Claims.Find(claimId);
        if (claim == null)
        {
            return NotFound();
        }

        claim.Status = "Rejected";
        _context.SaveChanges();

        return RedirectToAction("ViewClaims", new { managerId = claim.ManagerID });
    }
}
