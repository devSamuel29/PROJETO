using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Validators.Auth;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class RegisterUseCase : IRegisterUseCase
{
    public IAuthRepository _repository;

    public RegisterUseCase(IAuthRepository repository)
    {
        _repository = repository;
    }
    
    public Task<JwtSecurityToken> SignUp(SignUpRequest request)
    {
        try
        {
            var isValidRequest = SignUpRequestValidator.ValidateRequest(request);

            if (isValidRequest)
            {
                return _repository.SignUpAsync(request);
            }

            throw new Exception();
        }
        catch
        {
            throw new Exception();
        }
    }
}
