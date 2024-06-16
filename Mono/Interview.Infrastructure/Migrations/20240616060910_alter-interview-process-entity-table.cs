using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview.Infrastructure.Migrations
{
    public partial class alterinterviewprocessentitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Processes",
                table: "InterviewProcess",
                newName: "StepKey");

            migrationBuilder.AddColumn<string>(
                name: "Step",
                table: "InterviewProcess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Step",
                table: "InterviewProcess");

            migrationBuilder.RenameColumn(
                name: "StepKey",
                table: "InterviewProcess",
                newName: "Processes");
        }
    }
}
