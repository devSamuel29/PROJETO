using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Exceptions.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.Validators.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class RegisterUseCase : IRegisterUseCase
{
    public IAuthRepository _repository;

    private readonly ISignUpRequestValidator _signUpRequestValidator;

    public RegisterUseCase(
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

            return new ResultIdentity
            {
                StatusCode = StatusCodeIdentity.CREATED,
                Data = await _repository.SignUpAsync(request)
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
