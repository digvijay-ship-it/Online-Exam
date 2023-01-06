using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamWeb.Migrations
{
    /// <inheritdoc />
    public partial class resultColSubjectIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Results_SubjectId",
                table: "Results",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Subjects_SubjectId",
                table: "Results",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Subjects_SubjectId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_SubjectId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Results");
        }
    }
}
