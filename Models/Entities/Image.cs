using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurInstagram.Models.Entities;

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
    
    [DataType(DataType.DateTime)]
    public DateTime uploadTime { get; set; }
    
    // owner
    [ForeignKey("userId")]
    public int userId { get; set; }
    public User? user { get; set; }
    public ICollection<Like>? likes { get; set; }
    
    [NotMapped]
    public bool isLiked { get; set; }
}