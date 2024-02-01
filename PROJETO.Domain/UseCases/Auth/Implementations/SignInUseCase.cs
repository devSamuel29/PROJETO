using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
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

    public async Task<ResultResponse> SignIn(SignInRequest request)
    {
        try
        {
            ValidationResult validationResult = _signInRequestValidator.ValidateRequest(
                request
            );

            if (validationResult.HasNotification)
                return new ResultResponse
                {
                    StatusCode = StatusCodeIdentity.BAD_REQUEST,
                    Data = validationResult.Notifiers
                };

            JwtIdentity token = await _repository.SignInAsync(request);
            return new ResultResponse
            {
                StatusCode = StatusCodeIdentity.SUCCESS,
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
