using Microsoft.EntityFrameworkCore;

using Src.Domain.Models;
using Src.Infra.Database;
using Src.Infra.DataSources.SqlServer.User.Abstractions;

namespace Src.Infra.DataSources.SqlServer.User.Implementations;

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
        UserModel userModel = await _sqlServerContext.Users
            .Include(p => p.Role)
            .FirstAsync(m => m.Id == model.Id);

        return userModel;
    }

    public async Task<UserModel?> ReadByEmailAsync(string email)
    {
        UserModel userModel = await _sqlServerContext.Users
            .AsQueryable()
            .Include(p => p.Role)
            .Where(model => model.Email == email)
            .FirstAsync();

        return userModel;
    }
}
