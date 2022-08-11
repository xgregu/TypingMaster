using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingMaster.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TestType = table.Column<int>(type: "INTEGER", nullable: false),
                    TextToRewritten = table.Column<string>(type: "TEXT", nullable: false),
                    ExecutorName = table.Column<string>(type: "TEXT", nullable: false),
                    TestLenght = table.Column<int>(type: "INTEGER", nullable: false),
                    EffectivenessPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    ClickPerSecond = table.Column<double>(type: "REAL", nullable: false),
                    CompletionTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Mistakes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
