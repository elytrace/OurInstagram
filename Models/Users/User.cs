using OurInstagram.Models.Images;

namespace OurInstagram.Models.Users;

public class User
{
    public int userId { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }
    public string? email { get; set; }
    public string? phone { get; set; }
    public DateOnly dateOfBirth { get; set; }
    public bool gender { get; set; }
    public string? avatarPath { get; set; }
    public string? biography { get; set; }
    public string? displayedName { get; set; }
    
    public ICollection<Image> images { get; set; }
}