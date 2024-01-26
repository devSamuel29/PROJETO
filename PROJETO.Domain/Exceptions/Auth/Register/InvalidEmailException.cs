namespace PROJETO.Domain.Exceptions.Auth.Register;

public class InvalidEmailException : AuthException
{
    public InvalidEmailException()
        : base("The email provided is not valid!") { }
}
