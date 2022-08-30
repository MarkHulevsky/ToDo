namespace Users.BusinessLogic.Constants;

public static class AccountConstants
{
    /*--------Information--------*/
    public const string USER_CREATED_SUCCESSFULLY = "We will send a confirmation message to your email.";
    
    /*--------Errors--------*/
    public const string USER_ALREADY_EXISTS_ERROR = "User with provided email is already exists.";
    public const string USER_NOT_FOUND_ERROR = "User with provided email does not exists.";
    public const string INCORRECT_PASSWORD_ERROR = "Password is incorrect";
}