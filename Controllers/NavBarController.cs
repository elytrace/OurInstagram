using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;

namespace OurInstagram.Controllers;

public class NavBarController : Controller
{
    private readonly ILogger<NavBarController> _logger;
    private bool isSearching = false;

    public NavBarController(ILogger<NavBarController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ToHome()
    {
        return RedirectToAction("Index", "Home", isSearching);
    }

    public IActionResult ToExplore()
    {
        return RedirectToAction("Explore", "Home");
    }

    public IActionResult ToProfile()
    {
        return RedirectToAction("Index", "Profile");
    }
}