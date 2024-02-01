using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Models;

namespace PROJETO.Infra.Services.Jwt.Abstractions;

public interface IJwtService
{
    JwtSecurityToken GenerateAccessToken(List<Claim> authClaim);

    JwtSecurityToken GenerateAccessToken(UserModel model);

    string GenerateRefreshToken();

    IList<Claim> GenerateClaims(UserModel userModel);

    string JwtToString(JwtSecurityToken token);

    bool ValidateToken();
}
