using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Src.Domain.Models;

namespace Src.Infra.Services.Jwt.Abstractions;

public interface IJwtService
{
    JwtSecurityToken GenerateAccessToken(List<Claim> authClaim);

    JwtSecurityToken GenerateAccessToken(UserModel model);

    string GenerateRefreshToken();

    IList<Claim> GenerateClaims(UserModel userModel);

    bool ValidateToken();
}
