using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamWeb.Migrations
{
    /// <inheritdoc />
    public partial class resultAdded1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Options_OptionId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_OptionId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "Results",
                newName: "Answer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Results",
                newName: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_OptionId",
                table: "Results",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Options_OptionId",
                table: "Results",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
