using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class QuestionIdUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Teachers_TeacherId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Teachers_TeacherId",
                table: "Question",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Teachers_TeacherId",
                table: "Question");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Question",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Teachers_TeacherId",
                table: "Question",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
