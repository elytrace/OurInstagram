using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinsta.Models.Entities;

[Table("comments")]
public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int commentId { get; set; }
    
    [StringLength(500)]
    public string? comment { get; set; }
    public int userId { get; set; }
    [ForeignKey("userId")]
    public User user { get; set; }
    
    [ForeignKey("imageId")]
    public int imageId { get; set; }
    public Image image { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime timeStamp { get; set; }
}