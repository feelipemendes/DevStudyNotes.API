using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudyNotes.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyNoteReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPositive = table.Column<bool>(type: "bit", nullable: false),
                    StudyNoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyNoteReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyNoteReactions_StudyNotes_StudyNoteId",
                        column: x => x.StudyNoteId,
                        principalTable: "StudyNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyNoteReactions_StudyNoteId",
                table: "StudyNoteReactions",
                column: "StudyNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyNoteReactions");

            migrationBuilder.DropTable(
                name: "StudyNotes");
        }
    }
}
