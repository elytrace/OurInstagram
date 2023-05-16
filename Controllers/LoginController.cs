using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;

namespace OurInstagram.Controllers;

public class LoginController : Controller
{
    private readonly OurDbContext _db;

    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View("~/Views/Nguyen_Login/Index.cshtml");
    }

    public IActionResult Signup()
    {
        return View("~/Views/Nguyen_Login/Signup.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}