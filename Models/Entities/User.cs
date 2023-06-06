using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinsta.Models.Entities;

[Table("users")]
public class User
{
    public static User currentUser = new User();

    public User()
    {
        images = new List<Image>();
        followers = new List<User>();
        followings = new List<User>();
        likes = new List<Like>();
        comments = new List<Comment>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int userId { get; set; }
    
    [StringLength(50)]
    public string? username { get; set; }
    
    [StringLength(50)]
    public string? password { get; set; }
    
    [StringLength(50)]
    public string? email { get; set; }
    
    [StringLength(50)]
    public string? phone { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime dateOfBirth { get; set; }
    
    public byte gender { get; set; }
    
    [StringLength(255)]
    public string? avatarPath { get; set; }
    
    [StringLength(255)]
    public string? biography { get; set; }
    
    [StringLength(255)]
    public string? displayedName { get; set; }
    
    public ICollection<Image> images { get; set; }
    public ICollection<User> followers { get; set; }
    public ICollection<User> followings { get; set; }
    public ICollection<Like> likes { get; set; }
    
    public ICollection<Comment> comments { get; set; }
}