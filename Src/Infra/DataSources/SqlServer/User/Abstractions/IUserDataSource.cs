using Src.Domain.Models;

namespace Src.Infra.DataSources.SqlServer.User.Abstractions;

public interface IUserDataSource
{
    Task<UserModel> CreateAsync(UserModel model);

    Task<UserModel?> ReadByEmailAsync(string Email);
}
