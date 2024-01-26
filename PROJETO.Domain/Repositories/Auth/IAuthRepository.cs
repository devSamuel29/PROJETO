using System.IdentityModel.Tokens.Jwt;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<JwtSecurityToken> SignInAsync(SignInRequest request);

    Task<JwtSecurityToken> SignUpAsync(SignUpRequest request);
}
