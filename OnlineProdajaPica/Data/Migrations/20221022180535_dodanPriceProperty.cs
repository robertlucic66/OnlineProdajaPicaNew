using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineProdajaPica.Data.Migrations
{
    public partial class dodanPriceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductViewModel",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductViewModel");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");
        }
    }
}
