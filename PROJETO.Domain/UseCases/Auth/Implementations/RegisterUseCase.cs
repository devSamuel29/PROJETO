using PROJETO.Domain.Identities;
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

    public async Task<ResultIdentity> SignUp(SignUpRequest request)
    {
        try
        {
            bool isValidRequest = SignUpRequestValidator.ValidateRequest(request);

            if (isValidRequest)
            {
                return new ResultIdentity
                {
                    IsValid = true,
                    Data = await _repository.SignUpAsync(request)
                };
            }

            return new ResultIdentity { IsValid = false };
        }
        catch
        {
            throw new Exception();
        }
    }
}
