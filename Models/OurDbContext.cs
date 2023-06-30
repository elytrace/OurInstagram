using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pinsta.Controllers;
using Pinsta.Enums;
using Pinsta.Models.Entities;

namespace Pinsta.Models;

public class OurDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Image> images { get; set; }
    public DbSet<Like> likes { get; set; }
    public DbSet<Comment> comments { get; set; }
    public DbSet<SearchRecent> searchs { get; set; }

    // private const string connectionString = "server='localhost';userid=root;database=Pinsta;";
     private const string connectionString = "Server=k3n2-cnpm.mysql.database.azure.com;UserID = hieuhc;Password=Conm3otr3nc@y;Database=pinsta;default command timeout=0";

    // private const string mssqlConnectionString = "Server=ELYSIUM;Database=Pinsta;User Id=sa;Password=1;TrustServerCertificate=True;Encrypt=False;Trusted_Connection=SSPI";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySQL(
            connectionString,
            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            )
        );
        
        // optionsBuilder.UseSqlServer(mssqlConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasMany(left => left.followers)
            .WithMany(right => right.followings)
            .UsingEntity(join => join.ToTable("follows"));
        modelBuilder.Entity<User>()
            .HasMany(u => u.images)
            .WithOne(i => i.user)
            .HasForeignKey(i => i.userId);
        modelBuilder.Entity<User>()
            .HasMany(u => u.likes)
            .WithOne(l => l.user)
            .HasForeignKey(l => l.userId);
        modelBuilder.Entity<User>()
            .HasMany(u => u.comments)
            .WithOne(c => c.user)
            .HasForeignKey(c => c.userId);
        modelBuilder.Entity<User>()
            .HasMany(u => u.searchs)
            .WithOne(s => s.user)
            .HasForeignKey(s => s.userId);
        modelBuilder.Entity<Image>()
            .HasMany(i => i.likes)
            .WithOne(l => l.image)
            .HasForeignKey(l => l.imageId);
        modelBuilder.Entity<Image>()
            .HasMany(i => i.comments)
            .WithOne(c => c.image)
            .HasForeignKey(c => c.imageId);
    }
    
    public static async Task CreateDatabase()
    {
        await using var context = new OurDbContext();
        string databasename = context.Database.GetDbConnection().Database;
        
        Console.WriteLine("Creating " + databasename + "...");

        bool result = await context.Database.EnsureCreatedAsync();
        string resultstring = result ? "created succesfully!" : "has already existed!";
        Console.WriteLine($"Database {databasename}: {resultstring}");
        // Console.WriteLine("user: " + context.users.ToList().Count);
        // Console.WriteLine("image: " + context.images.ToList().Count);
        // Console.WriteLine("follow: " + context.follows.ToList().Count);
        // Console.WriteLine("like: " + context.likes.ToList().Count);
        // Console.WriteLine("comment: " + context.comments.ToList().Count);
        // Console.WriteLine("search: " + context.searchs.ToList().Count);
        // Console.WriteLine(context.users.FirstOrDefault(u => u.username == "hieuhc"));
        // RetrieveDatabase();
    }
    
    public static async Task DeleteDatabase()
    {
        using var context = new OurDbContext();
        string databasename = context.Database.GetDbConnection().Database;
        bool deleted = await context.Database.EnsureDeletedAsync();
        string deletionInfo = deleted ? "has been removed!" : "cannot be removed!";
        Console.WriteLine($"{databasename} {deletionInfo}");
    }
    
    public static async Task RetrieveData()
    {
        using var context = new OurDbContext();
        // follow
        Random gen = new Random();
        var userList = context.users.ToList();
        foreach (var user in userList)
        {
            user.followers = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
            user.followings = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
        }
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Follows");

        // image uploaded for each user
        var imageList = context.images;
        foreach (var image in imageList)
        {
            image.user = userList.FirstOrDefault(u => u.userId == image.userId);
        }
        await context.SaveChangesAsync();
        
        // image liked for each user
        var likeList = new List<Like>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(imageList.Count() / 2);
            int end = gen.Next(start + 1, imageList.Count());
            for (int j = start; j < end; j++)
            {
                likeList.Add(new Like { userId = i+1, imageId = j+1, timeStamp = DateTime.Now });
            }
        }
        await context.AddRangeAsync(likeList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Likes");

        foreach (var image in imageList.ToList())
        {
            await context.Entry(image).Collection(i => i.likes).LoadAsync();
        }
        await context.SaveChangesAsync();
        
        // image comments for each user
        var commentList = new List<Comment>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(imageList.Count() / 2);
            int end = gen.Next(start + 1, imageList.Count());
            for (int j = start; j < end; j++)
            {
                commentList.Add(new Comment { 
                    comment = gen.Next(2) == 1 ? "Tương tác." : "Ông đi qua bà đi lại con xin 1 follow please", 
                    userId = i+1, 
                    imageId = j+1, 
                    timeStamp = DateTime.Now 
                });
            }
        }
        await context.AddRangeAsync(commentList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Comments");

        foreach (var image in imageList.ToList())
        {
            await context.Entry(image).Collection(i => i.comments).LoadAsync();
        }
        await context.SaveChangesAsync();
        
        
        // search recent for each user
        var searchRecentList = new List<SearchRecent>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(userList.Count() / 2);
            int end = gen.Next(start + 1, userList.Count());
            for (int j = start; j < end; j++)
            {
                searchRecentList.Add(new SearchRecent() {
                    userId = i+1,
                    resultId = j+1,
                    timeStamp = DateTime.Now
                });
            }
        }
        await context.AddRangeAsync(searchRecentList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Searchs");

        foreach (var user in userList)
        {
            await context.Entry(user).Collection(u => u.searchs).LoadAsync();
        }
        await context.SaveChangesAsync();
    }

    public static async Task InsertSampleData()
    {
        using var context = new OurDbContext();
        var users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync("./SampleData/users.json"));
        await context.AddRangeAsync(users);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Users");

        var images = JsonConvert.DeserializeObject<List<Image>>(await File.ReadAllTextAsync("./SampleData/images.json"));
        await context.AddRangeAsync(images);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Images");

        // follow
        Random gen = new Random();
        var userList = context.users.ToList();
        foreach (var user in userList)
        {
            user.followers = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
            user.followings = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
        }
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Follows");

        // image uploaded for each user
        var imageList = context.images;
        foreach (var image in imageList)
        {
            image.user = userList.FirstOrDefault(u => u.userId == image.userId);
        }
        await context.SaveChangesAsync();
        
        // image liked for each user
        var likeList = new List<Like>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(imageList.Count() / 2);
            int end = gen.Next(start + 1, imageList.Count());
            for (int j = start; j < end; j++)
            {
                likeList.Add(new Like { userId = i+1, imageId = j+1, timeStamp = DateTime.Now });
            }
        }
        await context.AddRangeAsync(likeList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Likes");

        foreach (var image in imageList.ToList())
        {
            await context.Entry(image).Collection(i => i.likes).LoadAsync();
        }
        await context.SaveChangesAsync();
        
        // image comments for each user
        var commentList = new List<Comment>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(imageList.Count() / 2);
            int end = gen.Next(start + 1, imageList.Count());
            for (int j = start; j < end; j++)
            {
                commentList.Add(new Comment { 
                    comment = gen.Next(2) == 1 ? "Tương tác." : "Ông đi qua bà đi lại con xin 1 follow please", 
                    userId = i+1, 
                    imageId = j+1, 
                    timeStamp = DateTime.Now 
                });
            }
        }
        await context.AddRangeAsync(commentList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Comments");

        foreach (var image in imageList.ToList())
        {
            await context.Entry(image).Collection(i => i.comments).LoadAsync();
        }
        await context.SaveChangesAsync();
        
        
        // search recent for each user
        var searchRecentList = new List<SearchRecent>();
        for (int i = 0; i < userList.Count(); i++)
        {
            int start = gen.Next(userList.Count() / 2);
            int end = gen.Next(start + 1, userList.Count());
            for (int j = start; j < end; j++)
            {
                searchRecentList.Add(new SearchRecent() {
                    userId = i+1,
                    resultId = j+1,
                    timeStamp = DateTime.Now
                });
            }
        }
        await context.AddRangeAsync(searchRecentList);
        await context.SaveChangesAsync();
        Console.WriteLine("Finish inserting table Searchs");

        foreach (var user in userList)
        {
            await context.Entry(user).Collection(u => u.searchs).LoadAsync();
        }
        await context.SaveChangesAsync();
    }
    
    public LoginState ValidateLogin(string username, string password, OurDbContext context)
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
    
    public LoginState ValidateSignup(string username, string password, string confirmPassword, OurDbContext context)
    {
        if (password != confirmPassword) 
            return LoginState.WRONG_CONFIRM_PASSWORD;
        
        var user = context.users.FirstOrDefault(u => u.username == username);
        if (user != null) 
            return LoginState.USERNAME_EXISTED;

        return LoginState.SIGNUP_SUCCESS;
    }

    public void CreateNewUser(string username, string password, OurDbContext context)
    {
        var newUser = new User()
        {
            username = username, password = password
        };
        context.users.AddAsync(newUser);
        User.currentUser = newUser;
        context.SaveChangesAsync();
    }

    public void UploadImage(string url, int userID, OurDbContext context)
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