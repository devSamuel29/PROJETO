using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PROJETO.Domain.Models.User;

namespace PROJETO.Infra.Mappers;

public class RoleMapper : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnType("int")
            .HasColumnName("Id")
            .UseIdentityColumn();

        builder
            .Property(p => p.Name)
            .HasColumnType("varchar(20)")
            .HasColumnName("Name")
            .IsRequired();
        
        builder.HasData(
            new RoleModel { Id = 1, Name = "admin" },
            new RoleModel { Id = 2, Name = "user" }
        );
    }
}
