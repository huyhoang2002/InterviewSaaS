using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview.Infrastructure.Migrations
{
    public partial class companyjobCategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JobCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobCategory_CompanyId",
                table: "JobCategory",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategory_Company_CompanyId",
                table: "JobCategory",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCategory_Company_CompanyId",
                table: "JobCategory");

            migrationBuilder.DropIndex(
                name: "IX_JobCategory_CompanyId",
                table: "JobCategory");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobCategory");
        }
    }
}
