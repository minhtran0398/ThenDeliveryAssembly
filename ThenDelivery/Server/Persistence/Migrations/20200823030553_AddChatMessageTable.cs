using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class AddChatMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatId = table.Column<int>(nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendClientId = table.Column<string>(fixedLength: true, maxLength: 36, nullable: true),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => new { x.ChatId, x.SendTime });
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Client1Id = table.Column<string>(maxLength: 36, nullable: false),
                    Client2Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => new { x.Id, x.Client1Id, x.Client2Id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Chats");

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
    }
}
