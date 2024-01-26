namespace PROJETO.Domain.Models.User;

public class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public RoleModel Role { get; set; } = null!;
}
