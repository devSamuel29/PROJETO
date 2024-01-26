using PROJETO.Domain.Models.User;

namespace PROJETO.Infra.DataSources.Abstractions.SqlServer.Auth;

public interface IUserDataSource
{
    Task<UserModel> CreateAsync(UserModel model);

    Task<UserModel?> ReadByEmailAsync(string Email);
}
