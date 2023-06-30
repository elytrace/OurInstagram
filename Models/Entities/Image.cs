using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinsta.Models.Entities;

[Table("images")]
public class Image
{
    public Image()
    {
        likes = new HashSet<Like>();
        comments = new HashSet<Comment>();
    }

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
    public virtual User user { get; set; }
    public virtual ICollection<Like> likes { get; set; }
    public virtual ICollection<Comment> comments { get; set; }
    
    [NotMapped]
    public bool isLiked { get; set; }
}