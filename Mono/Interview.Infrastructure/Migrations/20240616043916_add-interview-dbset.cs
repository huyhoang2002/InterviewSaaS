using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview.Infrastructure.Migrations
{
    public partial class addinterviewdbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsApplied = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewProcess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Processes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewCollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewProcess_InterviewCollection_InterviewCollectionId",
                        column: x => x.InterviewCollectionId,
                        principalTable: "InterviewCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewCollection_CollectionName",
                table: "InterviewCollection",
                column: "CollectionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewProcess_InterviewCollectionId",
                table: "InterviewProcess",
                column: "InterviewCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewProcess");

            migrationBuilder.DropTable(
                name: "InterviewCollection");
        }
    }
}
