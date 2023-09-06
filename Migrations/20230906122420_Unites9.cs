using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSystem.Migrations
{
    /// <inheritdoc />
    public partial class Unites9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TabName",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabName",
                table: "Products");
        }
    }
}
