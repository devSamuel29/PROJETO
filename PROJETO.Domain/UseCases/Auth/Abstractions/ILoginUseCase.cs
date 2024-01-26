using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface ILoginUseCase 
{ 
    Task<ResultIdentity> SignIn(SignInRequest request);
}
