using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddUserSocialMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserSocialMedias");

            migrationBuilder.UpdateData(
                table: "UserSocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserSocialMedias",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserSocialMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "UserSocialMedias",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "UserId" },
                values: new object[] { "Aykut Öztürk", 2 });

            migrationBuilder.UpdateData(
                table: "UserSocialMedias",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "UserId" },
                values: new object[] { "Engin Demiroğ", 1 });
        }
    }
}
