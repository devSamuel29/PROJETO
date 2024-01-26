using System.IdentityModel.Tokens.Jwt;

using Microsoft.EntityFrameworkCore;

using PROJETO.Infra.Database;
using PROJETO.Domain.Models.User;
using PROJETO.Infra.DataSources.Abstractions.SqlServer.Auth;

namespace PROJETO.Infra.DataSources.Implementations;

public class UserDataSource : IUserDataSource
{
    private readonly SqlServerContext _sqlServerContext;

    public UserDataSource(SqlServerContext sqlServerContext)
    {
        _sqlServerContext = sqlServerContext;
    }

    public async Task<UserModel> CreateAsync(UserModel model)
    {
        var result = await _sqlServerContext.Users.AddAsync(model);
        await _sqlServerContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<UserModel?> ReadByEmailAsync(string email)
    {
        UserModel? userModel = await _sqlServerContext.Users.FirstOrDefaultAsync(
            user => user.Email == email
        );
        return userModel;
    }
}
