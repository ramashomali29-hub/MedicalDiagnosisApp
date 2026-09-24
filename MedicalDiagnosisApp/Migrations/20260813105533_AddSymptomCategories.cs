using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalDiagnosisApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSymptomCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Symptoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Symptoms");
        }
    }
}
