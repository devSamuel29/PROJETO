namespace PROJETO.Domain.Exceptions.Auth.Shared;

public class InvalidNameException : AuthException
{
    public InvalidNameException()
        : base(
            "The provided name must have between 5 and 20 characters and should not contains numbers or special characters."
        ) { }
}
