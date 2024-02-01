using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
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

    public async Task<ResultResponse> SignUp(SignUpRequest request)
    {
        try
        {
            ValidationResult result = _signUpRequestValidator.ValidateRequest(request);

            if (result.HasNotification)
                return new ResultResponse { StatusCode = 400, Data = result.Notifiers };

            JwtIdentity token = await _repository.SignUpAsync(request);
            return new ResultResponse
            {
                StatusCode = StatusCodeIdentity.CREATED,
                Data = token
            };
        }
        catch (Exception e)
        {
            return new ResultResponse
            {
                StatusCode = StatusCodeIdentity.BAD_REQUEST,
                Data = e.Message
            };
        }
    }
}
