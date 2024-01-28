using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface ISignUpUseCase
{
    Task<ResultIdentity> SignUp(SignUpRequest request);
}
