using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class SignUpUseCase : ISignUpUseCase
{
    public IAuthRepository _repository;

    private readonly ISignUpRequestValidator _signUpRequestValidator;

    public SignUpUseCase(
        IAuthRepository repository,
        ISignUpRequestValidator signUpRequestValidator
    )
    {
        _repository = repository;
        _signUpRequestValidator = signUpRequestValidator;
    }

    public async Task<ResultIdentity> SignUp(SignUpRequest request)
    {
        try
        {
            _signUpRequestValidator.ValidateRequest(request);
            JwtIdentity token = await _repository.SignUpAsync(request);
            return new ResultIdentity
            {
                StatusCode = StatusCodeIdentity.CREATED,
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
