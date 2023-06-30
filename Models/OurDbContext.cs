using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pinsta.Controllers;
using Pinsta.Enums;
using Pinsta.Models.Entities;

namespace Pinsta.Models;

public class OurDbContext : DbContext
{
    public static readonly OurDbContext context = new OurDbContext();
    public DbSet<User> users { get; set; }
    public DbSet<Image> images { get; set; }
    public DbSet<Like> likes { get; set; }
    public DbSet<Comment> comments { get; set; }
    public DbSet<SearchRecent> searchs { get; set; }

    // private const string connectionString = "server='localhost';userid=root;database=Pinsta;";
    private const string connectionString =
        "Server='pinsta-server.mysql.database.azure.com';UserID = 'hieuhc';Password='Conm3otr3nc@y';Database='pinsta'";

    // private const string mssqlConnectionString = "Server=ELYSIUM;Database=Pinsta;User Id=sa;Password=1;TrustServerCertificate=True;Encrypt=False;Trusted_Connection=SSPI";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies().UseMySQL(
            connectionString,
            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            ).CommandTimeout(120)
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
    
    public async Task CreateDatabase()
    {
        string databasename = Database.GetDbConnection().Database;
        
        Console.WriteLine("Creating " + databasename + "...");

        bool result = await Database.EnsureCreatedAsync();
        string resultstring = result ? "created succesfully!" : "has already existed!";
        Console.WriteLine($"Database {databasename}: {resultstring}");
        // RetrieveDatabase();
    }
    
    public async Task DeleteDatabase()
    {
        string databasename = Database.GetDbConnection().Database;
        bool deleted = await Database.EnsureDeletedAsync();
        string deletionInfo = deleted ? "has been removed!" : "cannot be removed!";
        Console.WriteLine($"{databasename} {deletionInfo}");
    }

    public async Task InsertSampleData()
    {
        var _users = JsonConvert.DeserializeObject<List<User>>(await File.ReadAllTextAsync("./SampleData/users.json"));
        await AddRangeAsync(_users);
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Users");

        var _images = JsonConvert.DeserializeObject<List<Image>>(await File.ReadAllTextAsync("./SampleData/images.json"));
        await AddRangeAsync(_images);
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Images");

        // follow
        Random gen = new Random();
        var userList = users.ToList();
        foreach (var user in userList)
        {
            user.followers = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
            user.followings = userList.Except(new[] { user }).OrderBy(u => Guid.NewGuid()).Take(gen.Next(userList.Count)).ToList();
        }
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Follows");

        // image uploaded for each user
        var imageList = images;
        foreach (var image in imageList)
        {
            image.user = userList.FirstOrDefault(u => u.userId == image.userId);
        }
        await SaveChangesAsync();
        
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
        await AddRangeAsync(likeList);
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Likes");

        foreach (var image in imageList.ToList())
        {
            await Entry(image).Collection(i => i.likes).LoadAsync();
        }
        await SaveChangesAsync();
        
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
        await AddRangeAsync(commentList);
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Comments");

        foreach (var image in imageList.ToList())
        {
            await Entry(image).Collection(i => i.comments).LoadAsync();
        }
        await SaveChangesAsync();
        
        
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
        await AddRangeAsync(searchRecentList);
        await SaveChangesAsync();
        Console.WriteLine("Finish inserting table Searchs");

        foreach (var user in userList)
        {
            await Entry(user).Collection(u => u.searchs).LoadAsync();
        }
        await SaveChangesAsync();
    }
    
    public LoginState ValidateLogin(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.username == username);
        if (user == null) 
            return LoginState.USERNAME_NOT_EXISTED;
        if (password != user.password) 
            return LoginState.WRONG_PASSWORD;

        Entry(user).Collection(u => u.images).LoadAsync();
        Entry(user).Collection(u => u.followers).LoadAsync();
        Entry(user).Collection(u => u.followings).LoadAsync();
        User.currentUser = user;
        return LoginState.LOGIN_SUCCESS;
    }
    
    public LoginState ValidateSignup(string username, string password, string confirmPassword)
    {
        if (password != confirmPassword) 
            return LoginState.WRONG_CONFIRM_PASSWORD;
        
        var user = users.FirstOrDefault(u => u.username == username);
        if (user != null) 
            return LoginState.USERNAME_EXISTED;

        return LoginState.SIGNUP_SUCCESS;
    }

    public void CreateNewUser(string username, string password)
    {
        var newUser = new User()
        {
            username = username, password = password
        };
        users.AddAsync(newUser);
        User.currentUser = newUser;
        SaveChangesAsync();
    }

    public void UploadImage(string url, int userID)
    {
        var newImage = new Image()
        {
            imagePath = url,
            caption = "Not yet implemented",
            userId = userID,
            uploadTime = DateTime.Now
        };
        images.Add(newImage);
        SaveChangesAsync();
        Entry(User.currentUser).Collection(u => u.images).LoadAsync();
    }
}