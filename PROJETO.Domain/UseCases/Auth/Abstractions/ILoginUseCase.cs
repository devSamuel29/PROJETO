using System.IdentityModel.Tokens.Jwt;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface ILoginUseCase 
{ 
    Task<JwtSecurityToken> SignIn(SignInRequest request);
}
