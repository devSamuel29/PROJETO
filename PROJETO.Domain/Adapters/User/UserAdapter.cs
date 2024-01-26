using Riok.Mapperly.Abstractions;

using PROJETO.Domain.Models.User;
using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Adapters.User;

[Mapper]
public partial class UserAdapter
{
    public partial UserModel RequestToModel(SignInRequest request);

    public partial UserModel RequestToModel(SignUpRequest request);
}
