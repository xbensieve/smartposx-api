using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPOSX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                table: "VariationImages");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "VariationImages");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "VariationImages");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "VariationImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "VariationImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "VariationImages");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "VariationImages");

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                table: "VariationImages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "VariationImages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FileSize",
                table: "VariationImages",
                type: "int",
                nullable: true);
        }
    }
}
