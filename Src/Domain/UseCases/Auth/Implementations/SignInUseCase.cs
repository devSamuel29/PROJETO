using Src.Domain.Response;
using Src.Domain.Identities;
using Src.Domain.Request.Auth;
using Src.Domain.Repositories.Auth;
using Src.Domain.Validators.Auth.Shared;
using Src.Domain.UseCases.Auth.Abstractions;
using Src.Domain.Validators.Auth.Abstractions;

namespace Src.Domain.UseCases.Auth.Implementations;

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

    public async Task<ResultResponse> Execute(SignInRequest request)
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

    Task<ResultResponse> ISignInUseCase.Execute(SignInRequest request)
    {
        throw new NotImplementedException();
    }
}
