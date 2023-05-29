using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurInstagram.Models.Login;

public class SignupInput
{
    [Required]
    [StringLength(50)]
    [DisplayName("Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [NotMapped]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; }
}