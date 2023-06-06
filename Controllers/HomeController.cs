using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Pinsta.Models;
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
        var followings = Models.Entities.User.currentUser.followings.Select(user => user.userId);
        var imageList = OurDbContext.context.images
            .Where(user => followings.Contains(user.userId)).ToList();
        
        return View(imageList);
    }

    [HttpPost]
    public ActionResult UploadImage(string imageURL)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(imageURL)
        };
        var uploadResult = new Cloudinary().Upload(uploadParams);
        Console.WriteLine("Upload OK. Result: " + uploadResult.JsonObj);
        OurDbContext.UploadImage(uploadResult.SecureUrl.ToString(), Models.Entities.User.currentUser.userId);
        return View("Index");
    }
}