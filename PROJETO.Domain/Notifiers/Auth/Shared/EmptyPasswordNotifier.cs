namespace PROJETO.Domain.Notifiers.Auth.Shared;

public class EmptyPasswordNotifier : AuthNotifier
{
    public EmptyPasswordNotifier()
        : base("Password", "The password is required!") { }
}
