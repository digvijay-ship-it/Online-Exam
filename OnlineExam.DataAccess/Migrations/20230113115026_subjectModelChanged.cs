using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamWeb.Migrations
{
    /// <inheritdoc />
    public partial class subjectModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Subject_Name",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "Subject_Name",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subject_Name",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Subject_Name",
                table: "Subjects",
                column: "Subject_Name",
                unique: true);
        }
    }
}
