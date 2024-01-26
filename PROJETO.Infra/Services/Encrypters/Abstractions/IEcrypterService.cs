namespace PROJETO.Infra.Services.Ecrypters.Abstractions;

public interface IEncrypterService
{
    string HashPassword(string password);

    string ValidateHash(string text, string hash);
}
