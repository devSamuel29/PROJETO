using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Validators.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class LoginUseCase : ILoginUseCase
{
    public IAuthRepository _repository;

    public LoginUseCase(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<JwtSecurityToken> SignIn(SignInRequest request)
    {
        JwtSecurityToken token = await _repository.SignInAsync(request);
        return token;
    }
}
