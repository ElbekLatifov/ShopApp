using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSystem.Migrations
{
    /// <inheritdoc />
    public partial class Unite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriceCome = table.Column<double>(type: "double", nullable: false),
                    NewPriceCome = table.Column<double>(type: "double", nullable: false),
                    PriceGo = table.Column<double>(type: "double", nullable: false),
                    NewPriceGo = table.Column<double>(type: "double", nullable: false),
                    Added_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    AddedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalProducts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalProducts");
        }
    }
}
