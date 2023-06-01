using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurInstagram.Models.Entities;

[Table("likes")]
public class Like
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int likeId { get; set; }
    
    public int userId { get; set; }
    public int imageId { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime timeStamp { get; set; }
}