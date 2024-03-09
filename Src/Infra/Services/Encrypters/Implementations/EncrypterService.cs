using Src.Infra.Services.Ecrypters.Abstractions;

namespace Src.Infra.Services.Ecrypters.Implementations;

public class EncrypterService : IEncrypterService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public string ValidateHash(string text, string hash)
    {
        throw new NotImplementedException();
    }
}
