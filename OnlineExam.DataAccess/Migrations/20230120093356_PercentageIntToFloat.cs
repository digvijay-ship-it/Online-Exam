using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExamWeb.Migrations
{
    /// <inheritdoc />
    public partial class PercentageIntToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "percentage",
                table: "UserSubjects",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "percentage",
                table: "UserSubjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
