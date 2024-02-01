using PROJETO.Domain.Identities;
using PROJETO.Domain.Notifiers.Auth;
using PROJETO.Domain.Notifiers.Auth.SignUp;
using PROJETO.Domain.Notifiers.Auth.Shared;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Validators.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.Validators.Implementations;

public class SignUpRequestValidator : ISignUpRequestValidator
{
    public ValidationResult ValidateRequest(SignUpRequest request)
    {
        ValidationResult validationResult = new ValidationResult();
        
        AuthNotifier? resultName = ValidateName(request.Name);
        AuthNotifier? resultEmail = ValidateEmail(request.Email);
        AuthNotifier? resultPassword = ValidatePassword(request.Password);
        AuthNotifier? resultBirthday = ValidateBirthDay(request.BirthDay);

        if (resultName is not null)
            validationResult.Notifiers.Add(resultName);

        if (resultEmail is not null)
            validationResult.Notifiers.Add(resultEmail);

        if (resultPassword is not null)
            validationResult.Notifiers.Add(resultPassword);

        if (resultBirthday is not null)
            validationResult.Notifiers.Add(resultBirthday);

        return validationResult;
    }

    public static AuthNotifier? ValidateName(string name)
    {
        if (name.Length <= 0)
            return new EmptyNameNotifier();

        if (AuthRegex.Number().IsMatch(name))
            return new InvalidNameNotifier();

        if (AuthRegex.SpecialChar().IsMatch(name))
            return new InvalidNameNotifier();

        int minLengthName = 5;

        if (name.Length < minLengthName)
            return new InvalidNameNotifier();

        int maxLengthName = 40;

        if (name.Length > maxLengthName)
            return new InvalidNameNotifier();

        return null;
    }

    public static AuthNotifier? ValidateEmail(string email)
    {
        if (email.Length <= 0)
            return new EmptyEmailNotifier();

        if (!AuthRegex.Email().IsMatch(email))
            return new InvalidEmailNotifier();

        return null;
    }

    public static AuthNotifier? ValidatePassword(string password)
    {
        if (password.Length <= 0)
            return new EmptyPasswordNotifier();

        try
        {
            password = System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(password)
            );
        }
        catch (FormatException)
        {
            return new Base64Notifier();
        }

        int minimumPasswordLenght = 8;

        if (password.Length < minimumPasswordLenght)
            return new InvalidPasswordNotifier();

        int maximumPasswordLenght = 128;

        if (password.Length > maximumPasswordLenght)
            return new InvalidPasswordNotifier();

        if (!AuthRegex.Number().IsMatch(password))
            return new InvalidPasswordNotifier();

        if (!AuthRegex.SpecialChar().IsMatch(password))
            return new InvalidPasswordNotifier();

        if (!AuthRegex.UpperCase().IsMatch(password))
            return new InvalidPasswordNotifier();

        return null;
    }

    public static AuthNotifier? ValidateBirthDay(string date)
    {
        if (!DateTime.TryParse(date, out DateTime result))
        {
            return new InvalidBirthDayNotifier();
        }

        if ((DateTime.Now - result).TotalDays < 365 * 18)
        {
            return new InvalidBirthDayNotifier();
        }

        return null;
    }
}
