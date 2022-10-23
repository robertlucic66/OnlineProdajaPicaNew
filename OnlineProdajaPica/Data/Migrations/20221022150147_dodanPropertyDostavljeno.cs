using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineProdajaPica.Data.Migrations
{
    public partial class dodanPropertyDostavljeno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Dostavljeno",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dostavljeno",
                table: "Orders");
        }
    }
}
