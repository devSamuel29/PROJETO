using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class SignInUseCase : ISignInUseCase
{
    private readonly IAuthRepository _repository;

    private readonly ISignInRequestValidator _signInRequestValidator;

    public SignInUseCase(
        IAuthRepository repository,
        ISignInRequestValidator signInRequestValidator
    )
    {
        _repository = repository;
        _signInRequestValidator = signInRequestValidator;
    }

    public async Task<ResultIdentity> SignIn(SignInRequest request)
    {
        try
        {
            _signInRequestValidator.ValidateRequest(request);

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
