namespace PROJETO.Domain.Exceptions.Auth.Shared;

public class InvalidPasswordException : AuthException
{
    public InvalidPasswordException()
        : base(
            @"The provided password must have between 8 and 128 characters, use at least one upper case letter, one special character (@, #, !, .) and one number."
        ) { }
}
