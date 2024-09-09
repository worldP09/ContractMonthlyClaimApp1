using Microsoft.AspNetCore.Mvc;
using CMCS.ViewModels;  // Correct namespace

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            WelcomeMessage = "Welcome to the Contract Monthly Claim System!"
        };

        return View(model);
    }
}
