using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;

namespace OurInstagram.Controllers;

public class LoginController : Controller
{
    private readonly OurDbContext _db;

    [BindProperty]
    public User user { get; set; }
    
    public LoginController(OurDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login()
    {
        //TODO: add authentication
        if (ModelState.IsValid)
        {
            
        }
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LoginWithFacebook()
    {
        //TODO: add authentication
        if (ModelState.IsValid)
        {
            
        }
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Signup()
    {
        return View("Signup");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}