using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OurInstagram.Controllers;
using OurInstagram.Enums;
using OurInstagram.Models.Entities;

namespace OurInstagram.Models;

public class OurDbContext : DbContext
{
    public static readonly OurDbContext context = new OurDbContext();
    public DbSet<User> users { get; set; }
    public DbSet<Image> images { get; set; }
    public DbSet<Like> likes { get; set; }

    private const string connectionString = "server=localhost;userid=root;database=ourinstagram;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySQL(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(left => left.followers)
            .WithMany(right => right.followings)
            .UsingEntity(join => join.ToTable("follows"));
    }

    public static async Task CreateDatabase()
    {
        string databasename = context.Database.GetDbConnection().Database;

        Console.WriteLine("Creating " + databasename + "...");

        bool result = await context.Database.EnsureCreatedAsync();
        string resultstring = result ? " created succesfully!" : " has already existed!";
        Console.WriteLine($"Database {databasename}: {resultstring}");
    }
    
    public static async Task DeleteDatabase()
    {
        string databasename = context.Database.GetDbConnection().Database;
        bool deleted = await context.Database.EnsureDeletedAsync();
        string deletionInfo = deleted ? "has been removed!" : "cannot be removed!";
        Console.WriteLine($"{databasename} {deletionInfo}");
    }

    public static async Task InsertSampleData()
    {
        var users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync("./SampleData/users.json"));
        await context.AddRangeAsync(users);
        await context.SaveChangesAsync();

        var images = JsonConvert.DeserializeObject<List<Image>>(await File.ReadAllTextAsync("./SampleData/images.json"));
        await context.AddRangeAsync(images);
        await context.SaveChangesAsync();

        // follow
        var userList = context.users.ToList();
        foreach (var user in userList)
        {
            user.followers = userList.Where(u => u.userId < user.userId).ToList();
            user.followings = userList.Where(u => u.userId > user.userId).ToList();
        }
        await context.SaveChangesAsync();

        // image uploaded for each user
        var imageList = context.images;
        foreach (var image in imageList)
        {
            await context.Entry(image).Reference(i => i.user).LoadAsync();
        }
        await context.SaveChangesAsync();
        
        // image liked for each user
        Random gen = new Random();
        var likeList = new List<Like>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(imageList.Count() / 2);
            int end = gen.Next(start + 1, imageList.Count());
            for (int j = start; j < end; j++)
            {
                likeList.Add(new Like { userId = i+1, imageId = j+1 });
            }
        }
        await context.AddRangeAsync(likeList);
        await context.SaveChangesAsync();

        foreach (var image in imageList)
        {
            context.Entry(image).Collection(i => i.likes).LoadAsync();
        }
        await context.SaveChangesAsync();
    }
    
    public static LoginState ValidateLogin(string username, string password)
    {
        var user = context.users.FirstOrDefault(u => u.username == username);
        if (user == null) 
            return LoginState.USERNAME_NOT_EXISTED;
        if (password != user.password) 
            return LoginState.WRONG_PASSWORD;

        context.Entry(user).Collection(u => u.images).LoadAsync();
        context.Entry(user).Collection(u => u.followers).LoadAsync();
        context.Entry(user).Collection(u => u.followings).LoadAsync();
        User.currentUser = user;
        return LoginState.LOGIN_SUCCESS;
    }
    
    public static LoginState ValidateSignup(string username, string password, string confirmPassword)
    {
        if (password != confirmPassword) 
            return LoginState.WRONG_CONFIRM_PASSWORD;
        
        var user = context.users.FirstOrDefault(u => u.username == username);
        if (user != null) 
            return LoginState.USERNAME_EXISTED;

        return LoginState.SIGNUP_SUCCESS;
    }

    public static void CreateNewUser(string username, string password)
    {
        var newUser = new User()
        {
            username = username, password = password
        };
        context.users.AddAsync(newUser);
        context.SaveChangesAsync();
    }

    public static void UploadImage(string url, int userID)
    {
        var newImage = new Image()
        {
            imagePath = url,
            caption = "Not yet implemented",
            userId = userID,
            uploadTime = DateTime.Now
        };
        context.images.Add(newImage);
        context.SaveChangesAsync();
        context.Entry(User.currentUser).Collection(u => u.images).LoadAsync();
    }
}