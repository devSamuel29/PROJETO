using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Notifiers.Auth;
using PROJETO.Domain.Notifiers.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.Validators.Implementations;

public class SignInRequestValidator : ISignInRequestValidator
{
    public ValidationResult ValidateRequest(SignInRequest request)
    {
        ValidationResult results = new ValidationResult();

        AuthNotifier? emailResult = ValidateEmail(request.Email);
        AuthNotifier? passwordResult = ValidatePassword(request.Password);

        if (emailResult is not null)
        {
            results.Notifiers.Add(emailResult);
        }
        
        if (passwordResult is not null)
        {
            results.Notifiers.Add(passwordResult);
        }

        return results;
    }

    private static AuthNotifier? ValidateEmail(string email)
    {
        if (email == string.Empty)
        {
            return new EmptyEmailNotifier();
        }

        if (!AuthRegex.Email().IsMatch(email))
        {
            return new InvalidEmailNotifier();
        }

        return null;
    }

    private static AuthNotifier? ValidatePassword(string password)
    {
        if (password.Length <= 0)
        {
            return new EmptyPasswordNotifier();
        }

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

        return null;
    }
}
