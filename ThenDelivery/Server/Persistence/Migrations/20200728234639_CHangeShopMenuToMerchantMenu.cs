using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
    public partial class CHangeShopMenuToMerchantMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreMenues");

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

            migrationBuilder.CreateTable(
                name: "MerchantMenues",
                columns: table => new
                {
                    MerchantMenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantMenues", x => x.MerchantMenuId);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b3358416-4cd6-4805-9c68-1827fb06ef30", "7395253e-4e87-423c-962b-17ec4d0c8c78", "User", null },
                    { "3deb04bc-c409-445e-a560-c3efce9a92eb", "11a59a09-c116-4de8-8c29-b703027dbdb1", "Shipper", null },
                    { "725f3944-c953-4832-b3be-a64558975c50", "f5ff7f03-882f-4a0a-908d-6ab6ff80a0d7", "Merchant", null },
                    { "f7525705-6c7d-431b-835e-11a87571f1e9", "fbbb8b3b-803f-478b-9b4d-7c9384a93852", "Administrator", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantMenues");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3deb04bc-c409-445e-a560-c3efce9a92eb");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "725f3944-c953-4832-b3be-a64558975c50");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b3358416-4cd6-4805-9c68-1827fb06ef30");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f7525705-6c7d-431b-835e-11a87571f1e9");

            migrationBuilder.CreateTable(
                name: "StoreMenues",
                columns: table => new
                {
                    StoreMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMenues", x => x.StoreMenuId);
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
    }
}
