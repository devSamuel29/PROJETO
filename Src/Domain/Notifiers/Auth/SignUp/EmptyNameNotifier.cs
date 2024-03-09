namespace Src.Domain.Notifiers.Auth.SignUp;

public class EmptyNameNotifier : AuthNotifier
{
    public EmptyNameNotifier()
        : base("Name", "The name is required!") { }
}
