using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pinsta.Models;
using Pinsta.Models.Entities;
using Pinsta.Models.Login;

namespace Pinsta.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        lock (new object())
        {
            var followings = Models.Entities.User.currentUser.followings.Select(user => user.userId);
            lock (new object())
            {
                var imageList = OurDbContext.context.images
                    .Where(user => followings.Contains(user.userId)).ToListAsync().Result;
        
                return View(imageList);
            }
        }
    }

    [HttpPost]
    public ActionResult UploadImage(string imageURL)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(imageURL)
        };
        var uploadResult = new Cloudinary("cloudinary://118591439573672:d6Me8w-RoHBAhA6lDUTmUnEaKcU@dy7yri3d9").Upload(uploadParams);
        Console.WriteLine("Upload OK. Result: " + uploadResult.JsonObj);
        OurDbContext.context.UploadImage(uploadResult.SecureUrl.ToString(), Models.Entities.User.currentUser.userId);
        return View("Index");
    }

    [HttpPost]
    public PartialViewResult Search()
    {
        var recentSearch = Models.Entities.User.currentUser.searchs.ToList();
        return PartialView("~/Views/Navbar/SearchPanel.cshtml", recentSearch);
    } 
}