namespace Src.Domain.Notifiers.Auth.SignUp;

public class InvalidNameNotifier : AuthNotifier
{
    public InvalidNameNotifier()
        : base("Name", "The name cannot contain special characters or numbers!") { }
}
