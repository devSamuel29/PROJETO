using System.Text.RegularExpressions;

using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Shared;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.Validators.Implementations;

public class SignInRequestValidator : ISignInRequestValidator
{
    public void ValidateRequest(SignInRequest request)
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

        bool isValidEmail = Regex.IsMatch(request.Email, AuthRegex.EMAIL_REGEX);

        if (!isValidEmail)
            throw new InvalidEmailException();

        string formattedEmail = request.Email.ToLower();

        request.Email = formattedEmail;
    }
}
