using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPOSX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Format",
                table: "VariationImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "VariationImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
