using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineProdajaPica.Data.Migrations
{
    public partial class updatedCustomerInfoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostNumber",
                table: "customerInfos",
                newName: "PostalCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "customerInfos",
                newName: "PostNumber");
        }
    }
}
