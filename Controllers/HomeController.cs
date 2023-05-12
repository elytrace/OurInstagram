using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;

namespace OurInstagram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("~/Views/Hiep_Home/Index.cshtml");
    }

    public IActionResult Explore()
    {
        return NotFound("Chức năng đang được thực hiện");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}