using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTech.Infrastructure.Migrations
{
    public partial class Traningisnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Tranings_TraningId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Tranings_TraningId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_TraningId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TraningId",
                table: "Exercises");

            migrationBuilder.AlterColumn<int>(
                name: "TraningId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ExerciseTraning",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "int", nullable: false),
                    TraningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTraning", x => new { x.ExercisesId, x.TraningId });
                    table.ForeignKey(
                        name: "FK_ExerciseTraning_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTraning_Tranings_TraningId",
                        column: x => x.TraningId,
                        principalTable: "Tranings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTraning_TraningId",
                table: "ExerciseTraning",
                column: "TraningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Tranings_TraningId",
                table: "Students",
                column: "TraningId",
                principalTable: "Tranings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Tranings_TraningId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "ExerciseTraning");

            migrationBuilder.AlterColumn<int>(
                name: "TraningId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TraningId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TraningId",
                table: "Exercises",
                column: "TraningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Tranings_TraningId",
                table: "Exercises",
                column: "TraningId",
                principalTable: "Tranings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Tranings_TraningId",
                table: "Students",
                column: "TraningId",
                principalTable: "Tranings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
