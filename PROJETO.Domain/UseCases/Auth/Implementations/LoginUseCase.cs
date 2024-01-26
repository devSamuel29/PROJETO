using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Validators.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Identities;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class LoginUseCase : ILoginUseCase
{
    public IAuthRepository _repository;

    public LoginUseCase(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultIdentity> SignIn(SignInRequest request)
    {
        try
        {
            bool isValid = SignInRequestValidator.ValidateRequest(request);
            if (isValid)
            {
                JwtIdentity token = await _repository.SignInAsync(request);
                return new ResultIdentity { IsValid = true, Data = token };
            }
            return new ResultIdentity { IsValid = false };
        }
        catch
        {
            throw new Exception();
        }
    }
}
