namespace PROJETO.Domain.Exceptions.Auth.Register;

public class InvalidNameException : AuthException
{
    public InvalidNameException()
        : base("The provided name must have between 5 and 20 characters.") { }
}
