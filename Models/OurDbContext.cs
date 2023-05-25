using Microsoft.EntityFrameworkCore;
using OurInstagram.Controllers;
using OurInstagram.Models.Images;
using OurInstagram.Models.Users;

namespace OurInstagram.Models;

public class OurDbContext : DbContext
{
    public static readonly OurDbContext context = new OurDbContext();
    public DbSet<User> users { get; set; }
    public DbSet<Image> images { get; set; }

    private const string connectionString = "server=localhost;userid=root;database=ourinstagram";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySQL(connectionString);
    }
    
    public static async Task CreateDatabase()
    {
        string databasename = context.Database.GetDbConnection().Database;

        Console.WriteLine("Tạo " + databasename);

        bool result = await context.Database.EnsureCreatedAsync();
        string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
        Console.WriteLine($"CSDL {databasename} : {resultstring}");
    }
    
    public static async Task DeleteDatabase()
    {
        string databasename = context.Database.GetDbConnection().Database;
        bool deleted = await context.Database.EnsureDeletedAsync();
        string deletionInfo = deleted ? "đã xóa" : "không xóa được";
        Console.WriteLine($"{databasename} {deletionInfo}");
    }

    public static async Task InsertSampleData()
    {
        await context.AddRangeAsync(
            new User()
            {
                username = "hieuhc@falcongames.com",
                password = "171114",
                email = "chihieuk50@gmail.com",
                phone = "0857639199",
                dateOfBirth = new DateTime(2002, 1, 17),
                gender = 1,
                avatarPath = "https://i.pinimg.com/736x/7f/2d/a9/7f2da9cdaaba31c68503277be1ee2d81.jpg",
                biography = "User test 1",
                displayedName = "Chí Hiếu"
            },
            new User()
            {
                username = "trangdh@gmail.com",
                password = "110402",
                email = "trangdh@gmail.com",
                phone = "0982352291",
                dateOfBirth = new DateTime(2002, 4, 11),
                gender = 0,
                avatarPath = "https://i.pinimg.com/736x/7f/2d/a9/7f2da9cdaaba31c68503277be1ee2d81.jpg",
                biography = "User test 2",
                displayedName = "Đặng Trang"
            }
        );
        await context.SaveChangesAsync();

        await context.AddRangeAsync(
            new Image()
            {
                imagePath = "https://i.pinimg.com/236x/e3/41/4b/e3414b2fcf00375a199ba6964be551af.jpg",
                caption = "image test 1",
                like = 0, userId = 1,
            },
            new Image()
            {
                imagePath = "https://i.pinimg.com/236x/05/65/20/05652045e57af33599557db9f23188c0.jpg",
                caption = "image test 2",
                like = 0, userId = 1,
            },
            new Image()
            {
                imagePath = "https://i.pinimg.com/236x/c5/83/53/c58353e15f32f3cbfc7cdcbcf0dc2f34--mango-coulis-m-sorry.jpg",
                caption = "image test 3",
                like = 0, userId = 1,
            },
            new Image()
            {
                imagePath = "https://i.pinimg.com/564x/94/43/b9/9443b93bd8773fec91bc1837e8424e8e.jpg",
                caption = "image test 4",
                like = 0, userId = 1,
            },
            new Image()
            {
                imagePath = "https://i.pinimg.com/564x/e6/8a/42/e68a42c2e530fbdf6b3ab2f379dcd384.jpg",
                caption = "image test 5",
                like = 0, userId = 1,
            }
        );
        await context.SaveChangesAsync();
    }
    
    public static bool ValidateLogin(string username, string password)
    {
        var user = context.users.FirstOrDefault(u => u.username == username);
        if (user == null) return false;
        if (password != user.password) return false;

        context.Entry(user).Collection(u => u.images).LoadAsync();
            
        User.currentUser = user;
        return true;
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
            like = 0,
            userId = userID
        };
        context.images.Add(newImage);
        context.SaveChangesAsync();
        context.Entry(User.currentUser).Collection(u => u.images).LoadAsync();

    }
}