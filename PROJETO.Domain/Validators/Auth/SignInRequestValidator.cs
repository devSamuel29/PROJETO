using System.Text.RegularExpressions;
using PROJETO.Domain.Exceptions.Auth.Register;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Validators.Auth;

public static class SignInRequestValidator
{
    public static bool ValidateRequest(SignInRequest request)
    {
        bool isValidBase64Password = !Regex.IsMatch(request.Password, AuthRegex.BASE_64_REGEX);

        if (!isValidBase64Password)
            throw new Base64Exception();

        request.Password = System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(request.Password)
        );

        if (!Regex.IsMatch(request.Email, AuthRegex.EMAIL_REGEX))
            throw new InvalidEmailException();

        return true;
    }
}
