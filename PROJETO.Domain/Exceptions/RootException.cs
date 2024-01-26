namespace PROJETO.Domain.Exceptions;

public class RootException : Exception
{
    public RootException(string message)
        : base(message: message) { }
}
