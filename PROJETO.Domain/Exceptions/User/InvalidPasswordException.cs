namespace PROJETO.Domain.Exceptions.User;

public class InvalidPasswordException : UserException
{
    public InvalidPasswordException()
        : base("") { }
}
