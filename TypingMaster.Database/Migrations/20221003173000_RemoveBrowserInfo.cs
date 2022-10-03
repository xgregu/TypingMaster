using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingMaster.Database.Migrations
{
    public partial class RemoveBrowserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAndroid",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsDesktop",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsIPad",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsIPadPro",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsIPhone",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsMobile",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "IsTablet",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "OsName",
                table: "Tests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAndroid",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDesktop",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIPad",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIPadPro",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIPhone",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMobile",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTablet",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OsName",
                table: "Tests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
