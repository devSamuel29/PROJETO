using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface ISignInUseCase
{
    Task<ResultResponse> SignIn(SignInRequest request);
}
