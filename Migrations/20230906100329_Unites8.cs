using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSystem.Migrations
{
    /// <inheritdoc />
    public partial class Unites8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Totalcount",
                table: "CashedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Totalcount",
                table: "CashedProducts");
        }
    }
}
