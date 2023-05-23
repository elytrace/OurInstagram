using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;

namespace OurInstagram.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View(Models.Users.User.currentUser);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}