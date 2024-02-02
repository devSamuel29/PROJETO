namespace PROJETO.Domain.Exceptions.User;

public class EmailAlreadyExistsException : UserException
{
    public EmailAlreadyExistsException()
        : base("") { }
}
