namespace PROJETO.Domain.Exceptions.Auth;

public class AuthException : RootException
{
    public AuthException(string message)
        : base(message: message) { }
}
