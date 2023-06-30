using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinsta.Models.Entities;

[Table("likes")]
public class Like
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int likeId { get; set; }
    
    public int userId { get; set; }
    public virtual User user { get; set; }
    
    public int imageId { get; set; }
    public virtual Image image { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime timeStamp { get; set; }
}