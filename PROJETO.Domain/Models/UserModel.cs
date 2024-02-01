using PROJETO.Domain.Request.Auth;

namespace PROJETO.Domain.Models;

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

    public static implicit operator UserModel(SignUpRequest request) =>
        new UserModel
        {
            Name = request.Name.ToLowerInvariant(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Password = System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(request.Password)
            ),
            BirthDay = DateTime.Parse(request.BirthDay).ToUniversalTime(),
        };
}
