using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OurInstagram.Models.Login;

public class LoginInput
{
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")] 
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }
}