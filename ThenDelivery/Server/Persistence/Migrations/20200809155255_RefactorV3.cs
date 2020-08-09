using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class RefactorV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderDetails",
                maxLength: 256,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b144d39-63d7-4c90-a863-f416977de057", "4f43815a-3b85-4f5f-8bac-8127664a544f", "User", "USER" },
                    { "e6b15c81-9d1d-4cd4-9490-987b941144a5", "74bb1552-441c-476d-a911-9c6164b34609", "Shipper", "SHIPPER" },
                    { "bacaddd3-0534-4f66-8bbc-d8496031530a", "28e78d82-2c33-4a1c-8aa8-8d4979a17843", "Merchant", "MERCHANT" },
                    { "f8e14d2e-3283-46d0-a584-e3b3acfb4489", "ad84e2bb-5b1b-4e96-a05e-345070ba15ab", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7b144d39-63d7-4c90-a863-f416977de057");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bacaddd3-0534-4f66-8bbc-d8496031530a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e6b15c81-9d1d-4cd4-9490-987b941144a5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f8e14d2e-3283-46d0-a584-e3b3acfb4489");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderDetails");

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
    }
}
