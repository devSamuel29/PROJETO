namespace Src.Domain.Notifiers.Auth.Shared;

public class EmptyEmailNotifier : AuthNotifier
{
    public EmptyEmailNotifier()
        : base("Email", "The email is required!") { }
}
