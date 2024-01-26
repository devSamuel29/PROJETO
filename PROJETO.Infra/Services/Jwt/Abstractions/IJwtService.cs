using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Models.User;

namespace PROJETO.Infra.Services.Jwt.Abstractions;

public interface IJwtService
{
    JwtSecurityToken GenerateToken(List<Claim> authClaim);

    List<Claim> GenerateClaims(UserModel userModel);

    bool ValidateToken();
}
