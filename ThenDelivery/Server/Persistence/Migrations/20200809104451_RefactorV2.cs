using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class RefactorV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "25b887be-a7dd-4022-85e1-ae2345aa45e9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7914b87e-43ef-401f-8793-42b754ca4bbf");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c89d04b7-f633-4c07-87d4-43ebdb312bc2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d7ac99ac-e4b5-4997-973e-6e3b835865dd");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Topping",
                type: "decimal(8, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "decimal(8, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetails",
                type: "decimal(8, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderDetailToppings",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false),
                    ToppingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailToppings", x => new { x.OrderDetailId, x.ToppingId });
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "823a7bb5-aa41-404f-ae34-a6e7ca74c658", "4038dcd7-fc6a-4ef1-82b0-2a178947be71", "User", "USER" },
                    { "05b31c65-2580-478b-a1a1-8a092965e556", "e6a1cb9f-14d1-42fd-aec5-a80313989d66", "Shipper", "SHIPPER" },
                    { "ad890cbb-0665-45e8-9692-acd8572d6607", "db014a1a-2ffe-4564-9e11-3621bdf12f92", "Merchant", "MERCHANT" },
                    { "e99ff99a-f9af-4370-80ff-65096bdece1b", "6a1ef7ae-8523-4ea9-8d96-cffaf4d4223d", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailToppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "05b31c65-2580-478b-a1a1-8a092965e556");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "823a7bb5-aa41-404f-ae34-a6e7ca74c658");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ad890cbb-0665-45e8-9692-acd8572d6607");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e99ff99a-f9af-4370-80ff-65096bdece1b");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Topping",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetails",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 0)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrderDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OrderDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d7ac99ac-e4b5-4997-973e-6e3b835865dd", "22e9c7e3-2bfa-4cd8-bdd7-b8b7ed9e1e27", "User", "USER" },
                    { "25b887be-a7dd-4022-85e1-ae2345aa45e9", "933a8d5d-f3fd-4cc0-beba-1420804cd40c", "Shipper", "SHIPPER" },
                    { "7914b87e-43ef-401f-8793-42b754ca4bbf", "8ce43217-e2c1-4e87-90b2-a2e90c52d764", "Merchant", "MERCHANT" },
                    { "c89d04b7-f633-4c07-87d4-43ebdb312bc2", "27bd26eb-23e7-46db-b58d-77b77b628da9", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
