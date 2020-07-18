using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
	{
		public void Configure(EntityTypeBuilder<ShippingAddress> builder)
		{
			builder.ToTable("ShippingAddresses");

			builder.HasKey(e => e.ShippingAddressId);

			builder.Property(e => e.UserId)
					.HasColumnName("UserId")
					.HasColumnType("nvarchar")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.CityCode)
					.HasColumnName("CityCode")
					.HasColumnType("char")
					.HasMaxLength(2)
					.IsRequired(true);

			builder.Property(e => e.DistrictCode)
					.HasColumnName("DistrictCode")
					.HasColumnType("char")
					.HasMaxLength(3)
					.IsRequired(true);

			builder.Property(e => e.WardCode)
					.HasColumnName("WardCode")
					.HasColumnType("char")
					.HasMaxLength(5)
					.IsRequired(true);

			builder.Property(e => e.HouseNumber)
					.HasColumnName("HouseNumber")
					.HasColumnType("nvarchar")
					.HasMaxLength(256)
					.IsRequired(true);
		}
	}
}
