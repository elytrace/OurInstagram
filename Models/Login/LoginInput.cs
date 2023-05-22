using System.ComponentModel.DataAnnotations;

namespace OurInstagram.Models.Login;

public class LoginInput
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")] 
    [DataType(DataType.Password)]
    public string Password { get; set; }
}