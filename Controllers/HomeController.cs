using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Pinsta.Models;
using Pinsta.Models.Entities;
using Pinsta.Models.Login;

namespace Pinsta.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        using var context = new OurDbContext();
        var followings = Models.Entities.User.currentUser.followings.Select(user => user.userId);
        var imageList = context.images
            .Where(user => followings.Contains(user.userId)).ToList();
        
        return View(imageList);
    }

    [HttpPost]
    public ActionResult UploadImage(string imageURL)
    {
        using var context = new OurDbContext();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(imageURL)
        };
        var uploadResult = new Cloudinary().Upload(uploadParams);
        Console.WriteLine("Upload OK. Result: " + uploadResult.JsonObj);
        context.UploadImage(uploadResult.SecureUrl.ToString(), Models.Entities.User.currentUser.userId, context);
        return View("Index");
    }

    [HttpPost]
    public PartialViewResult Search()
    {
        var recentSearch = Models.Entities.User.currentUser.searchs.ToList();
        return PartialView("~/Views/Navbar/SearchPanel.cshtml", recentSearch);
    } 
}