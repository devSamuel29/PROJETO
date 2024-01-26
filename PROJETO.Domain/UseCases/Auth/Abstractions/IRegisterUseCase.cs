using System.IdentityModel.Tokens.Jwt;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.UseCases.Auth.Abstractions;

public interface IRegisterUseCase
{
    Task<JwtSecurityToken> SignUp(SignUpRequest request);
}
