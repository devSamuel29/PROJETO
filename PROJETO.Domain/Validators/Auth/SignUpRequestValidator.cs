using System.Text.RegularExpressions;
using PROJETO.Domain.Exceptions.Auth.Register;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Validators.Auth;

public static class SignUpRequestValidator
{
    private static readonly string EMAIL_REGEX = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";

    private static readonly string NUMBER_REGEX = @"(?=.*\d)";

    private static readonly string SPECIAL_CHARACTER_REGEX = @"(?=.*[$*&@#])";

    private static readonly string UPPERCASE_REGEX = @"(?=.*[A-Z])";

    private static readonly string BASE_64_REGEX = @"^[-A-Za-z0-9+/]*={0,3}$";

    public static bool ValidateRequest(SignUpRequest request)
    {
        bool isValidBase64Password = Regex.IsMatch(request.Password, BASE_64_REGEX);

        if (!isValidBase64Password)
            throw new Base64Exception();

        request.Password = System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(request.Password)
        );

        int minimumPasswordLenght = 8;

        if (request.Password.Length < minimumPasswordLenght)
            throw new InvalidPasswordException();

        int maximumPasswordLenght = 128;

        if (request.Password.Length > maximumPasswordLenght)
            throw new InvalidPasswordException();

        bool hasNumberInPassword = Regex.IsMatch(request.Password, NUMBER_REGEX);

        if (!hasNumberInPassword)
            throw new InvalidPasswordException();

        bool hasSpecialChar = Regex.IsMatch(request.Password, SPECIAL_CHARACTER_REGEX);

        if (!hasSpecialChar)
            throw new InvalidPasswordException();

        bool hasUpperCase = Regex.IsMatch(request.Password, UPPERCASE_REGEX);

        if (!hasUpperCase)
            throw new InvalidPasswordException();

        bool isValidEmail = Regex.IsMatch(request.Email, EMAIL_REGEX);

        if (!isValidEmail)
            throw new InvalidEmailException();

        if (Regex.IsMatch(request.Name, NUMBER_REGEX))
            throw new Exception();

        if (Regex.IsMatch(request.Name, SPECIAL_CHARACTER_REGEX))
            throw new Exception();

        int minLengthName = 5;

        if (request.Name.Length < minLengthName)
            throw new Exception();

        int maxLengthName = 20;

        if (request.Name.Length > maxLengthName)
            throw new Exception();

        DateTime minDate = DateTime.MinValue;

        if (request.BirthDay < minDate)
            throw new Exception();

        DateTime maxData = DateTime.UtcNow.AddYears(-18);

        if (request.BirthDay > maxData)
            throw new Exception();

        return true;
    }
}
