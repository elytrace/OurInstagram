using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using OurInstagram.Models.Images;

namespace OurInstagram.Models.Users;

[Table("users")]
public class User
{
    public static User currentUser = new User();
    [Key]
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
    public DateOnly dateOfBirth { get; set; }
    
    public byte gender { get; set; }
    
    [StringLength(255)]
    public string? avatarPath { get; set; }
    
    [StringLength(255)]
    public string? biography { get; set; }
    
    [StringLength(255)]
    public string? displayedName { get; set; }
    
    public ICollection<Image> images { get; set; }
}