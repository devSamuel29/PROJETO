namespace Src.Domain.Request.Auth;

public class SignUpRequest
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string BirthDay { get; set; } = null!;
}
