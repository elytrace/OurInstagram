using OurInstagram.Models.Images;
using OurInstagram.Models.Login;
using OurInstagram.Models.Users;

namespace OurInstagram.Models;

public class LoginModel
{
    public LoginInput? LoginInput { get; set; }
    public SignupInput? SignupInput { get; set; }
}