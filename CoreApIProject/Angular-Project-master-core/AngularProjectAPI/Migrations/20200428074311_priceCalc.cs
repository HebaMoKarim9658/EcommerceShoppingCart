using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularProjectAPI.Migrations
{
    public partial class priceCalc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<short>(
                name: "Quantity",
                table: "OrderDetails",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
