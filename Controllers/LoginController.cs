using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Models;
using OurInstagram.Models.Login;
using OurInstagram.Models.Users;

namespace OurInstagram.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }
    
    public ActionResult Index()
    {
        // ModelState.Clear();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public ActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
        if (ModelState.ContainsKey("Confirm Password"))
            ModelState["Confirm Password"].Errors.Clear();
        
        if (ModelState.IsValid)
        {
            // user = MyDbContext.GetCurrentUser(model.LoginInput.Username, model.LoginInput.Password);
            var loginState = OurDbContext.ValidateLogin(model.LoginInput.Username, model.LoginInput.Password);
            if (loginState) 
                return RedirectToAction("Index", "Home");
            
            ModelState.Clear();
            ModelState.AddModelError("Password", "The user name or password is incorrect");
        }
        return View("Index", model);
    }

    [HttpPost]
    public ActionResult Signup(LoginModel model)
    {
        if (ModelState.ContainsKey("Password"))
            ModelState["Password"].Errors.Clear();
        
        if (ModelState.IsValid)
        {
            if (model.SignupInput.Password == model.SignupInput.ConfirmPassword)
            {
                OurDbContext.CreateNewUser(model.SignupInput.Username, model.SignupInput.Password);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Confirm Password", "The password and confirmation password do not match.");
        }
        return View("Index", model);
    }
    
    
}