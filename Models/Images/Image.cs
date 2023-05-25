using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OurInstagram.Models.Users;

namespace OurInstagram.Models.Images;

[Table("images")]
public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int imageId { get; set; }
    
    [StringLength(255)]
    public string? imagePath { get; set; }
    
    [StringLength(255)]
    public string? caption { get; set; }
    public int like { get; set; }
    
    [ForeignKey("users")]
    public int userId { get; set; }
    public User? user { get; set; }
}