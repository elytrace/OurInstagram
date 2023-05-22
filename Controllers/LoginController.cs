using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;
using OurInstagram.Models.Login;
using OurInstagram.Models.Users;

namespace OurInstagram.Controllers;

public class LoginController : Controller
{
    public static User? user;
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View("~/Views/Nguyen_Login/Index.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult? Login(LoginModel input)
    {
        if (ModelState.IsValid)
        {
            user = UserContext.GetCurrentUser(input.LoginInput.Email, input.LoginInput.Password);
            if (user != null) return RedirectToAction("Index", "Home");
            return NotFound("Không tồn tại user");
        }

        return null;
    }
    
    [HttpPost]
    public IActionResult Signup(LoginModel input)
    {
        if (ModelState.IsValid)
        {
            UserContext.CreateNewUser(input.SignupInput.Email, input.SignupInput.Password);
            return RedirectToAction("Index", "Home");
        }
        // else
        // {
        //     return View("~/Views/Nguyen_Login/Index.cshtml", input);
        // }
        //
        return View("~/Views/Nguyen_Login/Index.cshtml");
    }
}