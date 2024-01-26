using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using PROJETO.Domain.Models.User;
using PROJETO.Infra.Services.Jwt.Abstractions;

namespace PROJETO.Infra.Services.Jwt.Implementations;

public class JwtService : IJwtService
{
    public List<Claim> GenerateClaims(UserModel userModel)
    {
        return new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userModel.Name),
            new(JwtRegisteredClaimNames.Email, userModel.Email),
        };
    }

    public JwtSecurityToken GenerateToken(List<Claim> authClaim)
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

    public bool ValidateToken()
    {
        throw new NotImplementedException();
    }
}
