using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Validators.Auth;

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
            SignInRequestValidator.ValidateRequest(request);

            JwtIdentity token = await _repository.SignInAsync(request);
            return new ResultIdentity
            {
                StatusCode = StatusCodeIdentity.SUCCESS,
                Data = token
            };
        }
        catch (AuthException e)
        {
            return new ResultIdentity
            {
                StatusCode = StatusCodeIdentity.BAD_REQUEST,
                Data = e.Message
            };
        }
    }
}
