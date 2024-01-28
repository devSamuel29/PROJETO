using Microsoft.EntityFrameworkCore;

using PROJETO.Domain.Models.User;

using PROJETO.Infra.Database;
using PROJETO.Infra.DataSources.SqlServer.User.Abstractions;

namespace PROJETO.Infra.DataSources.SqlServer.User.Implementations;

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

    public async Task<UserModel> ReadByEmailAsync(string email)
    {
        UserModel userModel = await _sqlServerContext.Users.FirstAsync(
            user => user.Email == email
        );
        return userModel;
    }
}
