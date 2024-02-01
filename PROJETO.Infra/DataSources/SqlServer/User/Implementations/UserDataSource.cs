using PROJETO.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        return await ReadByEmailAsync(result.Entity.Email);
    }

    public async Task<UserModel> ReadByEmailAsync(string email)
    {
        UserModel userModel = await _sqlServerContext.Users
            .AsQueryable()
            .Include(p => p.Role)
            .Where(model => model.Email == email)
            .FirstAsync();

        return userModel;
    }
}
