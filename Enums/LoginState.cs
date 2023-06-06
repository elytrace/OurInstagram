namespace Pinsta.Enums;

public enum LoginState
{
    // login
    LOGIN_SUCCESS = 0,
    USERNAME_NOT_EXISTED,
    WRONG_PASSWORD,
    
    // sign up
    SIGNUP_SUCCESS,
    USERNAME_EXISTED,
    WRONG_CONFIRM_PASSWORD
}