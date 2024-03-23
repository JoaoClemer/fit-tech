using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTech.Infrastructure.Migrations
{
    public partial class GymPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Plans_PlanId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PlanId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "StudentPlanId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentPlanId",
                table: "Students",
                column: "StudentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_GymId",
                table: "Plans",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPlans_PlanId",
                table: "StudentPlans",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Gyms_GymId",
                table: "Plans",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentPlans_StudentPlanId",
                table: "Students",
                column: "StudentPlanId",
                principalTable: "StudentPlans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Gyms_GymId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentPlans_StudentPlanId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentPlans");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentPlanId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Plans_GymId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "StudentPlanId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Plans",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Plans",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PlanId",
                table: "Students",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Plans_PlanId",
                table: "Students",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
