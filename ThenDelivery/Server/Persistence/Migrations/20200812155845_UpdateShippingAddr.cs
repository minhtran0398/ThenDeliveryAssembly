using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThenDelivery.Server.Persistence.Migrations
{
	public partial class UpdateShippingAddr : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
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

		protected override void Down(MigrationBuilder migrationBuilder)
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
	}
}
