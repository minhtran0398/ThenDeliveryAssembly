using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThenDelivery.Server.Persistence.Migrations
{
	public partial class UpdateOrderDeleveryTime : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "406bc413-08c7-4311-9a96-cfd540591334");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "89f5aea7-4b77-49ea-96c8-e04adf882373");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "af214a51-c097-46fc-ad6d-aedc611d70fe");

			migrationBuilder.DeleteData(
				 table: "Roles",
				 keyColumn: "Id",
				 keyValue: "d33f4da0-1cad-4f04-9da8-e4cfde37139a");

			migrationBuilder.DropColumn(
				 name: "DeliveryDateTime",
				 table: "ShippingAddresses");

			migrationBuilder.AddColumn<DateTime>(
				 name: "DeliveryDateTime",
				 table: "Orders",
				 type: "datetime2",
				 nullable: false,
				 defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

		protected override void Down(MigrationBuilder migrationBuilder)
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

			migrationBuilder.DropColumn(
				 name: "DeliveryDateTime",
				 table: "Orders");

			migrationBuilder.AddColumn<DateTime>(
				 name: "DeliveryDateTime",
				 table: "ShippingAddresses",
				 type: "datetime2",
				 nullable: false,
				 defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.InsertData(
				 table: "Roles",
				 columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				 values: new object[,]
				 {
						  { "89f5aea7-4b77-49ea-96c8-e04adf882373", "8676c8c1-6a02-4ef1-acd2-82c6c992c981", "User", "USER" },
						  { "af214a51-c097-46fc-ad6d-aedc611d70fe", "a146b798-92b8-48f8-9b4e-40955ef27efe", "Shipper", "SHIPPER" },
						  { "d33f4da0-1cad-4f04-9da8-e4cfde37139a", "44ae84e1-ba45-48f2-835a-931f8217d14a", "Merchant", "MERCHANT" },
						  { "406bc413-08c7-4311-9a96-cfd540591334", "ce5af9ce-302c-4902-bea7-711faf0d912d", "Administrator", "ADMINISTRATOR" }
				 });
		}
	}
}
