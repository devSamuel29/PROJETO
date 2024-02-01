namespace PROJETO.Domain.Notifiers.Auth.SignUp;

public class InvalidPasswordNotifier : AuthNotifier
{
    public InvalidPasswordNotifier()
        : base(
            "Password",
            "The password must be between 8 and 128 characters, also one special character, one uppercase letter and one number!"
        ) { }
}
