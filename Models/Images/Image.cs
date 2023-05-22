using System.ComponentModel.DataAnnotations.Schema;
using OurInstagram.Models.Users;

namespace OurInstagram.Models.Images;

public class Image
{
    public int imageId { get; set; }
    public string? imagePath { get; set; }
    public int like { get; set; }
    
    public int userId { get; set; }
    public User? user { get; set; }
}