namespace Src.Domain.Identities;

public class JwtIdentity
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
