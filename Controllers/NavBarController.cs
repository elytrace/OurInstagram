using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OurInstagram.Models;
using OurInstagram.Models.Entities;

namespace OurInstagram.Controllers;

public class NavBarController : Controller
{
    private readonly ILogger<NavBarController> _logger;

    public NavBarController(ILogger<NavBarController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ToHome()
    {
        return RedirectToAction("Index", "Home");
    }

    public IActionResult ToProfile()
    {
        return RedirectToAction("Index", "Profile", new { Models.Entities.User.currentUser.username});
    }
    
    [HttpPost]
    public ActionResult DisplayImage(int imageId)
    {
        var image = OurDbContext.context.images.FirstOrDefault(image => image.imageId == imageId);
        image.isLiked = image.likes.Select(like => like.userId).Contains(Models.Entities.User.currentUser.userId);
        return PartialView(image);
    }
    
    [HttpPost]
    public ActionResult LikeImage(int imageId)
    {
        var image = OurDbContext.context.images.FirstOrDefault(image => image.imageId == imageId);
        if (!image.isLiked)
        {
            image.likes.Add(new Like
            {
                imageId = imageId, 
                userId = Models.Entities.User.currentUser.userId, 
                timeStamp = DateTime.Now
            });
        }
        else
        {
            var itemToRemove = image.likes.FirstOrDefault(u => u.userId == Models.Entities.User.currentUser.userId);
            if (itemToRemove != null) image.likes.Remove(itemToRemove);
        }
        image.isLiked = !image.isLiked;
        OurDbContext.context.SaveChangesAsync();
        
        return Json(image.likes.Count);
    }
    
    [HttpPost]
    public ActionResult CommentImage(string comment, int imageId)
    {
        var image = OurDbContext.context.images.FirstOrDefault(image => image.imageId == imageId);
        var newComment = new Comment
        {
            imageId = imageId,
            userId = Models.Entities.User.currentUser.userId,
            comment = comment,
            timeStamp = DateTime.Now
        };
        image.comments.Add(newComment);
        OurDbContext.context.SaveChangesAsync();
        
        return Json(image.comments.Count);
    }
    
    [HttpPost]
    public ActionResult DeleteImage(int imageId)
    {
        var imageToDelete = OurDbContext.context.images.FirstOrDefault(image => image.imageId == imageId);
        OurDbContext.context.Remove(imageToDelete);
        OurDbContext.context.SaveChangesAsync();
        OurDbContext.context.Entry(Models.Entities.User.currentUser).Collection(u => u.images).LoadAsync();

        return RedirectToAction("Index", "Profile", new { Models.Entities.User.currentUser.username });
    }
}