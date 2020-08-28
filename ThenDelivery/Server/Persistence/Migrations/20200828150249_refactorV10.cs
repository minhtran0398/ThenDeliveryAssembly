using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class refactorV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ToppingPrice",
                table: "OrderDetailToppings",
                type: "decimal(8, 0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToppingPrice",
                table: "OrderDetailToppings");
        }
    }
}
