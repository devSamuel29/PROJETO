namespace PROJETO.Domain.Exceptions.Auth.Register;

public class Base64Exception : AuthException
{
    public Base64Exception()
        : base("Password must be in Base64 encrypted!") { }
}
