namespace Src.Domain.Notifiers.Auth.Shared;

public class InvalidEmailNotifier : AuthNotifier
{
    public InvalidEmailNotifier()
        : base("Email", "The provided email is not valid!") { }
}
