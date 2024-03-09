using Microsoft.EntityFrameworkCore;

using Src.Domain.Identities;
using Src.Domain.Models;
using Src.Domain.Request.Auth;
using Src.Domain.Repositories.Auth;
using Src.Domain.Exceptions.User;

using Src.Infra.Services.Jwt.Abstractions;
using Src.Infra.Services.Ecrypters.Abstractions;
using Src.Infra.DataSources.SqlServer.User.Abstractions;

namespace Src.Infra.Repositories.Auth;

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
            UserModel? model =
                await _userDataSource.ReadByEmailAsync(request.Email)
                ?? throw new Exception();

            bool result = BCrypt.Net.BCrypt.Verify(
                text: request.Password,
                hash: model.Password
            );

            if (!result)
                throw new InvalidPasswordException();

            string accessToken = _jwtService.GenerateAccessToken(model).ToString();
            string refreshToken = _jwtService.GenerateRefreshToken();

            return new JwtIdentity
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<JwtIdentity> SignUpAsync(SignUpRequest request)
    {
        try
        {
            UserModel userModel = request;
            userModel.Password = _encrypterService.HashPassword(
                password: userModel.Password
            );
            UserModel createdUser = await _userDataSource.CreateAsync(model: userModel);

            string accessToken = _jwtService
                .GenerateAccessToken(model: createdUser)
                .ToString();
            string refreshToken = _jwtService.GenerateRefreshToken();

            return new JwtIdentity
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        catch (DbUpdateException)
        {
            throw new EmailAlreadyExistsException();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
