namespace PROJETO.Domain.Validators.Auth;

public static class AuthRegex
{
    public static readonly string EMAIL_REGEX = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";

    public static readonly string NUMBER_REGEX = @"(?=.*\d)";

    public static readonly string SPECIAL_CHARACTER_REGEX = @"(?=.*[$*&@#])";

    public static readonly string UPPERCASE_REGEX = @"(?=.*[A-Z])";

    public static readonly string BASE_64_REGEX = @"^[-A-Za-z0-9+/]*={0,3}$";
}
