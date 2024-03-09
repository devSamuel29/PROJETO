using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using Src.Domain.Models;
using Src.Domain.Identities.Shared;
using Src.Infra.Services.Jwt.Abstractions;

namespace Src.Infra.Services.Jwt.Implementations;

public class JwtService : IJwtService
{
    public IList<Claim> GenerateClaims(UserModel userModel)
    {
        return new List<Claim>()
        {
            new(JwtClaimsNameIdentity.Id, userModel.Id.ToString()),
            new(JwtClaimsNameIdentity.Name, userModel.Name),
            new(JwtClaimsNameIdentity.Email, userModel.Email),
            new(JwtClaimsNameIdentity.BirthDay, userModel.BirthDay.ToString()),
            new(JwtClaimsNameIdentity.Role, userModel.Role.Name)
        };
    }

    public JwtSecurityToken GenerateAccessToken(UserModel model)
    {
        IList<Claim> authClaims = GenerateClaims(model);
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                @"f@upa4!n#fv&OCfF$nU6pw&Temrl@iGcEMFX8lkd7ib7dTm^XzFHzit%VNLlHADlS*V7psJXYNxlSnv$9r!rXS!JwFngh6rCQLGSpLYXtUNw$BlG1rZJpFE1G@PwDiy5"
            )
        );

        var token = new JwtSecurityToken(
            issuer: "localhost",
            audience: "localhost",
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return token;
    }

    public JwtSecurityToken GenerateAccessToken(List<Claim> authClaim)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                @"f@upa4!n#fv&OCfF$nU6pw&Temrl@iGcEMFX8lkd7ib7dTm^XzFHzit%VNLlHADlS*V7psJXYNxlSnv$9r!rXS!JwFngh6rCQLGSpLYXtUNw$BlG1rZJpFE1G@PwDiy5"
            )
        );

        var token = new JwtSecurityToken(
            issuer: "localhost",
            audience: "localhost",
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaim,
            signingCredentials: new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public bool ValidateToken()
    {
        throw new NotImplementedException();
    }

    public string ToString(JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

