using Microsoft.EntityFrameworkCore.Migrations;

namespace izBackend.Migrations
{
    public partial class RefreshTokens2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "OriginalToken",
                table: "RefreshTokens",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "OriginalToken",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }
    }
}
