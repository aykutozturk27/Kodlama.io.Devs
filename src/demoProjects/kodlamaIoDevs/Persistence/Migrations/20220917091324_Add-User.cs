using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status" },
                values: new object[] { 1, 1, "administrator@administrator.com", "ADMINISTRATOR", "ADMINISTRATOR", new byte[] { 97, 100, 109, 105, 110, 105, 115, 116, 114, 97, 116, 111, 114 }, new byte[] { 97, 100, 109, 105, 110, 105, 115, 116, 114, 97, 116, 111, 114 }, true });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status" },
                values: new object[] { 2, 1, "user@user.com", "USER", "USER", new byte[] { 117, 115, 101, 114 }, new byte[] { 117, 115, 101, 114 }, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
