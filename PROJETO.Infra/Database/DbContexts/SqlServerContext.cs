using Microsoft.EntityFrameworkCore;

using PROJETO.Domain.Models.User;

namespace PROJETO.Infra.Database;

public class SqlServerContext : DbContext
{
    public SqlServerContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Initial Catalog=master;User Id=sa;Password=@Sdfl29052003;Encrypt=True;TrustServerCertificate=True"
        );
    }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<RoleModel> Roles { get; set; }
}
