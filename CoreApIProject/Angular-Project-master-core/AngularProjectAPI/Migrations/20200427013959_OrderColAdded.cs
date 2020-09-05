using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularProjectAPI.Migrations
{
    public partial class OrderColAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checkout",
                table: "OrderDetails");

            migrationBuilder.AddColumn<bool>(
                name: "checkout",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checkout",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "checkout",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
