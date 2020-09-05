using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularProjectAPI.Migrations
{
    public partial class orderMG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "checkout",
                table: "OrderDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checkout",
                table: "OrderDetails");
        }
    }
}
