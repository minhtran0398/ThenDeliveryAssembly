using Microsoft.EntityFrameworkCore.Migrations;

namespace ThenDelivery.Server.Persistence.Migrations
{
	public partial class RefactorDbV5 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "46d2c8f1-b4c1-4aa8-8f52-72f7e476748a");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "53c72f77-8a4e-4bcb-8915-9bd3d2306b06");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "c8327ec2-2e4d-4828-9e1f-95a8abdd6d6e");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "ead1d7da-5359-451c-9717-27032e850aaa");

			migrationBuilder.AddColumn<string>(
				 name: "FullName",
				 table: "ShippingAddresses",
				 maxLength: 256,
				 nullable: false,
				 defaultValue: "");

			migrationBuilder.AddColumn<string>(
				 name: "PhoneNumber",
				 table: "ShippingAddresses",
				 type: "nchar(10)",
				 nullable: false,
				 defaultValue: "");

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

		protected override void Down(MigrationBuilder migrationBuilder)
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

			migrationBuilder.DropColumn(
				 name: "FullName",
				 table: "ShippingAddresses");

			migrationBuilder.DropColumn(
				 name: "PhoneNumber",
				 table: "ShippingAddresses");

			migrationBuilder.InsertData(
				 table: "Roles",
				 columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				 values: new object[,]
				 {
						  { "46d2c8f1-b4c1-4aa8-8f52-72f7e476748a", "77728e20-2f52-46e4-8520-d68572b6cab5", "User", "USER" },
						  { "ead1d7da-5359-451c-9717-27032e850aaa", "a4a44c05-c905-4ae3-a8b4-208763bde965", "Shipper", "SHIPPER" },
						  { "c8327ec2-2e4d-4828-9e1f-95a8abdd6d6e", "bba915c3-1a1c-45dd-82a1-cf73fce7400b", "Merchant", "MERCHANT" },
						  { "53c72f77-8a4e-4bcb-8915-9bd3d2306b06", "368cba6c-1d43-4d26-9848-5acbca61369f", "Administrator", "ADMINISTRATOR" }
				 });
		}
	}
}
