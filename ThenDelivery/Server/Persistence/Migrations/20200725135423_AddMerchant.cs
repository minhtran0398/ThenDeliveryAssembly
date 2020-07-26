using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class AddMerchant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0f1fbf4e-1b0c-42cb-ada4-b2b845d8b8da");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2d567007-aa95-43a0-83de-f6d7002ab189");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "87900f5c-4055-49ca-b966-874f8ea827be");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f1e72b88-0ec1-4405-89c2-702f06916f49");

            migrationBuilder.CreateTable(
                name: "FeaturedDishCategoryMerchants",
                columns: table => new
                {
                    MerchantId = table.Column<int>(nullable: false),
                    FeaturedDishCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedDishCategoryMerchants", x => new { x.MerchantId, x.FeaturedDishCategoryId });
                });

            migrationBuilder.CreateTable(
                name: "MerchantTypeMerchants",
                columns: table => new
                {
                    MerchantId = table.Column<int>(nullable: false),
                    MerchantTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantTypeMerchants", x => new { x.MerchantId, x.MerchantTypeId });
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b3263af-e195-4878-aa9d-e8a274266a32", "a3474038-544c-4cd9-8fcb-2010d24f0271", "User", null },
                    { "5ae59482-8505-4929-9241-7bc067862334", "60efc2be-050a-4033-82fd-a88503901152", "Shipper", null },
                    { "20a92679-ab49-4aa2-aef5-49acb9e67898", "f875ff8f-8fc8-4794-a2c9-801d0d10a1d0", "Merchant", null },
                    { "752f25ec-a3e1-4ec3-b9e6-bd4b5be642dd", "87a56b1e-6a6e-4ae2-93a7-90e0334ca768", "Administrator", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedDishCategoryMerchants");

            migrationBuilder.DropTable(
                name: "MerchantTypeMerchants");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "20a92679-ab49-4aa2-aef5-49acb9e67898");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5ae59482-8505-4929-9241-7bc067862334");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6b3263af-e195-4878-aa9d-e8a274266a32");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "752f25ec-a3e1-4ec3-b9e6-bd4b5be642dd");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f1fbf4e-1b0c-42cb-ada4-b2b845d8b8da", "ac4de763-aada-4d2d-a5fa-9218e648aa35", "User", null },
                    { "87900f5c-4055-49ca-b966-874f8ea827be", "a8a3b910-4df0-48dd-9689-5a60e97f77d1", "Shipper", null },
                    { "f1e72b88-0ec1-4405-89c2-702f06916f49", "c624c6d0-c0dd-4ad4-87a5-6858a020b594", "Merchant", null },
                    { "2d567007-aa95-43a0-83de-f6d7002ab189", "3873ff06-a85d-4d4d-9b20-5318f5b5b391", "Administrator", null }
                });
        }
    }
}
