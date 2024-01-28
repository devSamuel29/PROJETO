namespace PROJETO.Domain.Exceptions.Auth.Shared;

public class InvalidEmailException : AuthException
{
    public InvalidEmailException()
        : base("The email provided is not valid!") { }
}
