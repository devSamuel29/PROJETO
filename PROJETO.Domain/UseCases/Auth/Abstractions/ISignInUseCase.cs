using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface ISignInUseCase 
{ 
    Task<ResultIdentity> SignIn(SignInRequest request);
}
