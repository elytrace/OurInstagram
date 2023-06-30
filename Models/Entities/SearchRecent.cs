using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pinsta.Enums;

namespace Pinsta.Models.Entities;

[Table("searchs")]
public class SearchRecent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int searchId { get; set; }
    // who search?
    public int userId { get; set; }
    public virtual User user { get; set; }
    // search who?
    public int resultId { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime timeStamp { get; set; }
}