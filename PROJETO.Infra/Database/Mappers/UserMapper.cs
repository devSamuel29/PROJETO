using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROJETO.Domain.Enuns.Role;
using PROJETO.Domain.Models.User;

namespace PROJETO.Infra.Mappers;

public class UserMapper : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn();

        builder
            .Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Password)
            .HasColumnName("Password")
            .HasColumnType("varchar(84)")
            .IsRequired();

        builder
            .Property(p => p.BirthDay)
            .HasColumnName("BirthDay")
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()");

        builder
            .Property(p => p.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.RoleId).HasDefaultValue(ERole.User);

        builder
            .HasOne(p => p.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(p => p.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
