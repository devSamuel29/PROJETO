using System.Text.RegularExpressions;

using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.Validators.Implementations;

public class SignUpRequestValidator : ISignUpRequestValidator
{
    public void ValidateRequest(SignUpRequest request)
    {
        try
        {
            string decodedPassword = System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(request.Password)
            );

            request.Password = decodedPassword;
        }
        catch (FormatException)
        {
            throw new Base64Exception();
        }

        int minimumPasswordLenght = 8;

        bool hasMinPasswordLenght = request.Password.Length < minimumPasswordLenght;

        if (hasMinPasswordLenght)
            throw new InvalidPasswordException();

        int maximumPasswordLenght = 128;

        bool hasMaxPasswordLength = request.Password.Length > maximumPasswordLenght;

        if (hasMaxPasswordLength)
            throw new InvalidPasswordException();

        bool hasNumberInPassword = Regex.IsMatch(
            request.Password,
            AuthRegex.NUMBER_REGEX
        );

        if (!hasNumberInPassword)
            throw new InvalidPasswordException();

        bool hasSpecialChar = Regex.IsMatch(
            request.Password,
            AuthRegex.SPECIAL_CHARACTER_REGEX
        );

        if (!hasSpecialChar)
            throw new InvalidPasswordException();

        bool hasUpperCase = Regex.IsMatch(request.Password, AuthRegex.UPPERCASE_REGEX);

        if (!hasUpperCase)
            throw new InvalidPasswordException();

        bool isValidEmail = Regex.IsMatch(request.Email, AuthRegex.EMAIL_REGEX);

        if (!isValidEmail)
            throw new InvalidEmailException();

        string formattedEmail = request.Email.Trim().ToLower();

        request.Email = formattedEmail;

        bool hasNumberInName = Regex.IsMatch(request.Name, AuthRegex.NUMBER_REGEX);

        if (hasNumberInName)
            throw new InvalidNameException();

        bool hasSpecialCharInName = Regex.IsMatch(
            request.Name,
            AuthRegex.SPECIAL_CHARACTER_REGEX
        );

        if (hasSpecialCharInName)
            throw new InvalidNameException();

        int minLengthName = 5;

        bool hasMinNameLength = request.Name.Length < minLengthName;

        if (hasMinNameLength)
            throw new InvalidNameException();

        int maxLengthName = 40;

        bool hasMaxNameLength = request.Name.Length > maxLengthName;

        if (hasMaxNameLength)
            throw new InvalidNameException();

        string formattedName = request.Name.ToLower();

        request.Name = formattedName;

        DateTime minDate = DateTime.MinValue;

        bool hasMinDate = request.BirthDay < minDate;

        if (hasMinDate)
            throw new InvalidBirthDateException();

        DateTime maxData = DateTime.UtcNow.AddYears(-18);

        bool hasMaxDate = request.BirthDay > maxData;

        if (hasMaxDate)
            throw new InvalidBirthDateException();
    }
}
