namespace PROJETO.Domain.Identities;

public class JwtIdentity
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
