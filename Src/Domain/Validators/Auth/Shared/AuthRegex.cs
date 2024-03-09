using System.Text.RegularExpressions;

namespace Src.Domain.Validators.Auth.Shared;

public static partial class AuthRegex
{
    [GeneratedRegex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$")]
    public static partial Regex Email();

    [GeneratedRegex(@"\d")]
    public static partial Regex Number();

    [GeneratedRegex(@"(?=.*[$*&@#])")]
    public static partial Regex SpecialChar();

    [GeneratedRegex(@"(?=.*[A-Z])")]
    public static partial Regex UpperCase();
}
