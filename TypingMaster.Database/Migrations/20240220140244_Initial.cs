using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingMaster.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CultureCode = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypingLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DifficultyLevel = table.Column<uint>(type: "INTEGER", nullable: false),
                    DifficultyCoefficient = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypingTestStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EffectivenessPercentage = table.Column<long>(type: "INTEGER", nullable: false),
                    ClickPerMinute = table.Column<double>(type: "REAL", nullable: false),
                    CompletionTimeMilliseconds = table.Column<long>(type: "INTEGER", nullable: false),
                    TotalClicks = table.Column<long>(type: "INTEGER", nullable: false),
                    MistakesClicks = table.Column<long>(type: "INTEGER", nullable: false),
                    OverallRating = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingTestStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslationInLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Translation = table.Column<string>(type: "TEXT", nullable: false),
                    CultureId = table.Column<long>(type: "INTEGER", nullable: false),
                    TranslationEntityId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationInLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationInLanguages_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranslationInLanguages_Translations_TranslationEntityId",
                        column: x => x.TranslationEntityId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypingTexts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    CultureId = table.Column<long>(type: "INTEGER", nullable: false),
                    DifficultyLevelId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypingTexts_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypingTexts_TypingLevels_DifficultyLevelId",
                        column: x => x.DifficultyLevelId,
                        principalTable: "TypingLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypingTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecutorName = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    TextId = table.Column<long>(type: "INTEGER", nullable: false),
                    StatisticsId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastChangeDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypingTests_TypingTestStatistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "TypingTestStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypingTests_TypingTexts_TextId",
                        column: x => x.TextId,
                        principalTable: "TypingTexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationInLanguages_CultureId",
                table: "TranslationInLanguages",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationInLanguages_TranslationEntityId",
                table: "TranslationInLanguages",
                column: "TranslationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingTests_StatisticsId",
                table: "TypingTests",
                column: "StatisticsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypingTests_TextId",
                table: "TypingTests",
                column: "TextId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingTexts_CultureId",
                table: "TypingTexts",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingTexts_DifficultyLevelId",
                table: "TypingTexts",
                column: "DifficultyLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationInLanguages");

            migrationBuilder.DropTable(
                name: "TypingTests");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "TypingTestStatistics");

            migrationBuilder.DropTable(
                name: "TypingTexts");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "TypingLevels");
        }
    }
}
