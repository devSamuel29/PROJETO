namespace Src.Domain.Notifiers.Auth.SignUp;

public class MaxPasswordNotifier : AuthNotifier
{
    public MaxPasswordNotifier()
        : base("Password", "") { }
}
