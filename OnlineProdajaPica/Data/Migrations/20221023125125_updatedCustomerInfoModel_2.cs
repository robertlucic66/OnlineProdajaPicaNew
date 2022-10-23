using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineProdajaPica.Data.Migrations
{
    public partial class updatedCustomerInfoModel_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "customerInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "customerInfos");
        }
    }
}
