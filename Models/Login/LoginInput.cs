using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OurInstagram.Models.Login;

public class LoginInput
{
    [Required]
    [StringLength(50)]
    [DisplayName("Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")] 
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }
}