using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurInstagram.Enums;
using OurInstagram.Models;
using OurInstagram.Models.Login;

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
        return View();
    }
    public PartialViewResult LandingPage()
    {
        return PartialView();
    }
    public PartialViewResult LoginForm()
    {
        return PartialView();
    }
    public PartialViewResult SignupForm()
    {
        return PartialView();
    }

    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
        model.SignupInput = null;
        ModelState["Signup Error"]?.Errors.Clear();

        if (ModelState.IsValid)
        {
            var status = OurDbContext.ValidateLogin(model.LoginInput.Username, model.LoginInput.Password);
            if (status == LoginState.LOGIN_SUCCESS)
                return RedirectToAction("Index", "Home");
            if (status == LoginState.WRONG_PASSWORD)
            {
                ModelState.Clear();
                ModelState.AddModelError("Login Error", "The user name or password is incorrect!");
            }
            if (status == LoginState.USERNAME_NOT_EXISTED)
            {
                ModelState.Clear();
                ModelState.AddModelError("Login Error", "Username does not existed!");
            }
        }
        return View("Index", model);
    }

    [HttpPost]
    public ActionResult Signup(LoginModel model)
    {
        model.LoginInput = null;
        ModelState["Login Error"]?.Errors.Clear();
        
        if (ModelState.IsValid)
        {
            var status = OurDbContext.ValidateSignup(model.SignupInput.Username, model.SignupInput.Password, model.SignupInput.ConfirmPassword);
            if (status == LoginState.SIGNUP_SUCCESS)
            {
                OurDbContext.CreateNewUser(model.SignupInput.Username, model.SignupInput.Password);
                return RedirectToAction("Index", "Home");
            }
            if (status == LoginState.USERNAME_EXISTED)
            {
                ModelState.Clear();
                ModelState.AddModelError("Signup Error", "Username existed!");
            }
            if (status == LoginState.WRONG_CONFIRM_PASSWORD)
            {
                ModelState.Clear();
                ModelState.AddModelError("Signup Error", "The password and confirmation password do not match.");
            }
        }
        return View("Index", model);
    }
}