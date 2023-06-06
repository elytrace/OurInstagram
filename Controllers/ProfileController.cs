using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pinsta.Models;

namespace Pinsta.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }
    
    // public IActionResult Index()
    // {
    //     return View(Models.Entities.User.currentUser);
    // }
    
    public IActionResult Index(string username)
    {
        var user = OurDbContext.context.users.FirstOrDefault(u => u.username == username);
        return View(user);
    }

    [HttpPost]
    public ActionResult Follow(int userId)
    {
        var userToFollow = OurDbContext.context.users.FirstOrDefault(u => u.userId == userId);
        if (userToFollow.followers.Contains(Models.Entities.User.currentUser))
        {
            userToFollow.followers.Remove(Models.Entities.User.currentUser);
        }
        else
        {
            userToFollow.followers.Add(Models.Entities.User.currentUser);
        }
        OurDbContext.context.SaveChangesAsync();
        return Json(userToFollow.followers.Count);
    }
}