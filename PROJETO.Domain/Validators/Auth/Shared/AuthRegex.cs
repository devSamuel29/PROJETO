namespace PROJETO.Domain.Validators.Auth.Shared;

public static class AuthRegex
{
    public const string EMAIL_REGEX = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";

    public const string NUMBER_REGEX = @"(?=.*\d)";

    public const string SPECIAL_CHARACTER_REGEX = @"(?=.*[$*&@#])";

    public const string UPPERCASE_REGEX = @"(?=.*[A-Z])";
}
