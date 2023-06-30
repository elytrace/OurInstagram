using System.Diagnostics;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Pinsta.Models;
using Pinsta.Models.Entities;

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
        using var context = new OurDbContext();
        var user = context.users.FirstOrDefault(u => u.username == username);
        return View(user);
    }

    public ActionResult FollowerPanel(int userId = 0)
    {
        using var context = new OurDbContext();
        if (userId == 0)
        {
            return PartialView(Models.Entities.User.currentUser.followers?.ToList());
        }
        var followerList = context.users
            .FirstOrDefault(u => u.userId == userId)?
            .followers?.ToList();
        return PartialView(followerList);
    }
    
    public ActionResult FollowingPanel(int userId = 0)
    {
        using var context = new OurDbContext();
        if (userId == 0)
        {
            return PartialView(Models.Entities.User.currentUser.followings?.ToList());
        }
        var followingList = context.users
            .FirstOrDefault(u => u.userId == userId)?
            .followings?.ToList();
        return PartialView(followingList);
    }

    [HttpPost]
    public ActionResult ConfirmEditImage(int imageId, string imageUrl)
    {
        using var context = new OurDbContext();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(imageUrl)
        };
        var uploadResult = new Cloudinary().Upload(uploadParams);
        Console.WriteLine("ImageID: " + imageId + ". Upload OK. Result: " + uploadResult.JsonObj);
        if (imageId == -1)
        {
            Models.Entities.User.currentUser.avatarPath = uploadResult.SecureUrl.ToString();
        }
        else if (imageId == -2)
        {
            context.UploadImage(uploadResult.SecureUrl.ToString(), Models.Entities.User.currentUser.userId, context);
        }
        else
        {
            context.images
                .FirstOrDefault(i => i.imageId == imageId)!.imagePath = uploadResult.SecureUrl.ToString();
        }

        context.SaveChangesAsync();
        return Json(uploadResult.SecureUrl.ToString());
    }

    [HttpPost]
    public ActionResult Follow(int userId)
    {
        using var context = new OurDbContext();
        var userToFollow = context.users.FirstOrDefault(u => u.userId == userId);
        if (userToFollow.followers.Contains(Models.Entities.User.currentUser))
        {
            userToFollow.followers.Remove(Models.Entities.User.currentUser);
        }
        else
        {
            userToFollow.followers.Add(Models.Entities.User.currentUser);
        }
        context.SaveChangesAsync();
        return Json(userToFollow.followers.Count);
    }
    
    [HttpPost]
    public ActionResult FollowDirectly(int userId)
    {
        using var context = new OurDbContext();
        var followState = false;
        var userToFollow = context.users.FirstOrDefault(u => u.userId == userId);
        if (userToFollow.followers.Contains(Models.Entities.User.currentUser))
        {
            userToFollow.followers.Remove(Models.Entities.User.currentUser);
            followState = false;
        }
        else
        {
            userToFollow.followers.Add(Models.Entities.User.currentUser);
            followState = true;
        }
        context.SaveChangesAsync();
        return Json(new
        {
            cnt = Models.Entities.User.currentUser.followings?.Count,
            following = followState
        });
    }
}