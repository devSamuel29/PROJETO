using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJETO.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DefaultValueOnUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 26, 3, 3, 11, 260, DateTimeKind.Utc).AddTicks(6276),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 26, 3, 3, 11, 260, DateTimeKind.Utc).AddTicks(6020),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 26, 3, 3, 11, 260, DateTimeKind.Utc).AddTicks(6276));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 20,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 26, 3, 3, 11, 260, DateTimeKind.Utc).AddTicks(6020));
        }
    }
}
