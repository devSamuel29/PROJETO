using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<JwtIdentity> SignInAsync(SignInRequest request);

    Task<JwtIdentity> SignUpAsync(SignUpRequest request);
}
 