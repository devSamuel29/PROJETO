namespace Src.Domain.Exceptions.User;

public class UserException : RootException
{
    public UserException(string message)
        : base(message) { }
}
