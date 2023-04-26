using System.ComponentModel.DataAnnotations;

namespace OurInstagram.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string Username { get; set; }
    public string Password { get; set; }
}