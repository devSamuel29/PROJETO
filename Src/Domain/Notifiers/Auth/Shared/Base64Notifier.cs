namespace Src.Domain.Notifiers.Auth.Shared;

public class Base64Notifier : AuthNotifier
{
    public Base64Notifier()
        : base("Password", "The password is not base64 encrypted!") { }
}
