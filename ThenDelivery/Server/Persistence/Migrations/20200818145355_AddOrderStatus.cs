using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class AddOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "17e0f6e9-9767-4867-bb4d-cda85034bcac");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2a5460f1-fed5-4351-a576-306c840472d0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bda33fe1-783c-46eb-8444-309fdc06cd2c");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eec8546f-c8dc-4b4f-9d56-72c0858f59d4");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d518171-aca6-4ce5-965f-b6eb3cdd458b", "ce7ac91d-1558-427b-b979-6996dd349191", "User", "USER" },
                    { "d7a7bdbb-6726-40d0-9722-a4b0bf6a6dfb", "103cc950-0207-4fd9-82ff-061328d8ecc0", "Shipper", "SHIPPER" },
                    { "2b74b5ab-a514-4c7f-b44e-78350bbbe4ad", "e091feb1-15cf-4eae-a55c-f4df632d9695", "Merchant", "MERCHANT" },
                    { "8762a798-639c-46e0-a9a8-9731bd40bd52", "ba4c639e-4488-459c-82cd-cd2f5edc6b7c", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2b74b5ab-a514-4c7f-b44e-78350bbbe4ad");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8762a798-639c-46e0-a9a8-9731bd40bd52");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8d518171-aca6-4ce5-965f-b6eb3cdd458b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d7a7bdbb-6726-40d0-9722-a4b0bf6a6dfb");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a5460f1-fed5-4351-a576-306c840472d0", "9608202d-8fa0-4c74-9f76-b3455a671be6", "User", "USER" },
                    { "eec8546f-c8dc-4b4f-9d56-72c0858f59d4", "dc9e22ad-7a04-4c40-a32a-614a90c1b86f", "Shipper", "SHIPPER" },
                    { "17e0f6e9-9767-4867-bb4d-cda85034bcac", "3552523c-03f2-453d-9de9-c216b68c6d25", "Merchant", "MERCHANT" },
                    { "bda33fe1-783c-46eb-8444-309fdc06cd2c", "0c0ee1c5-ee7f-4e9d-b9e0-512c3163931c", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
