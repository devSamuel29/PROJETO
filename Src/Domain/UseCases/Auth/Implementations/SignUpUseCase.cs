using Src.Domain.Response;
using Src.Domain.Identities;
using Src.Domain.Request.Auth;
using Src.Domain.Repositories.Auth;
using Src.Domain.Validators.Auth.Shared;
using Src.Domain.UseCases.Auth.Abstractions;
using Src.Domain.Validators.Auth.Abstractions;

namespace Src.Domain.UseCases.Auth.Implementations;

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

    public async Task<ResultResponse> Execute(SignUpRequest request)
    {
        try
        {
            ValidationResult result = _signUpRequestValidator.ValidateRequest(request);

            if (result.HasNotification)
                return new ResultResponse
                {
                    StatusCode = StatusCodeIdentity.BAD_REQUEST,
                    Data = result.Notifiers
                };

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
