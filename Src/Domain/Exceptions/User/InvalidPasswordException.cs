namespace Src.Domain.Exceptions.User;

public class InvalidPasswordException : UserException
{
    public InvalidPasswordException()
        : base("") { }
}
