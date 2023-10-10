using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePortalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubjectSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionSubject",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionSubject",
                table: "Sessions");
        }
    }
}
