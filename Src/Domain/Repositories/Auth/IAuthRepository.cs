using Src.Domain.Identities;
using Src.Domain.Request.Auth;

namespace Src.Domain.Repositories.Auth;

public interface IAuthRepository
{
    Task<JwtIdentity> SignInAsync(SignInRequest request);

    Task<JwtIdentity> SignUpAsync(SignUpRequest request);
}
 