
namespace PROJETO.Domain.Models;

public class RoleModel
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public IList<UserModel>? Users { get; set; }
}
