using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

public class CoordinatorController : Controller
{
    private readonly CMCSContext _context;
    private readonly ILogger<CoordinatorController> _logger;

    public CoordinatorController(CMCSContext context, ILogger<CoordinatorController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Dashboard
    public IActionResult Dashboard(int coordinatorId)
    {
        var coordinator = _context.Coordinators.FirstOrDefault(c => c.CoordinatorID == coordinatorId);
        if (coordinator == null)
        {
            _logger.LogWarning("Coordinator not found: {CoordinatorID}", coordinatorId);
            return NotFound();
        }
        return View(coordinator);
    }
}
