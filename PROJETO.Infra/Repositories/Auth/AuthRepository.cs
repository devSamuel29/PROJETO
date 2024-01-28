using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using PROJETO.Domain.Identities;
using PROJETO.Domain.Models.User;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Adapters.User;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Infra.Services.Jwt.Abstractions;
using PROJETO.Infra.DataSources.Abstractions.SqlServer.Auth;
using PROJETO.Infra.Services.Ecrypters.Abstractions;

namespace PROJETO.Infra.Repositories.Auth;

public sealed class AuthRepository : IAuthRepository
{
    private readonly IEncrypterService _encrypterService;

    private readonly IUserDataSource _userDataSource;

    private readonly IJwtService _jwtService;

    public AuthRepository(
        IEncrypterService encrypterService,
        IUserDataSource userDataSource,
        IJwtService jwtService
    )
    {
        _encrypterService = encrypterService;
        _userDataSource = userDataSource;
        _jwtService = jwtService;
    }

    public async Task<JwtIdentity> SignInAsync(SignInRequest request)
    {
        try
        {
            UserModel? model = await _userDataSource.ReadByEmailAsync(request.Email);

            if (model is not null)
            {
                bool result = BCrypt.Net.BCrypt.Verify(
                    text: request.Password,
                    hash: model.Password
                );

                if (!result)
                    throw new Exception();

                List<Claim> userClaims = _jwtService.GenerateClaims(userModel: model);
                JwtSecurityToken token = _jwtService.GenerateToken(userClaims);
                return new JwtIdentity { Token = _jwtService.JwtToString(token) };
            }

            throw new Exception();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<JwtIdentity> SignUpAsync(SignUpRequest request)
    {
        try
        {
            UserModel userModel = new UserAdapter().RequestToModel(request: request);
            userModel.Password = _encrypterService.HashPassword(userModel.Password);
            UserModel createdUser = await _userDataSource.CreateAsync(model: userModel);

            List<Claim> userClaims = _jwtService.GenerateClaims(userModel: createdUser);
            JwtSecurityToken token = _jwtService.GenerateToken(userClaims);
            return new JwtIdentity { Token = _jwtService.JwtToString(token) };
        }
        catch
        {
            throw new Exception();
        }
    }
}
