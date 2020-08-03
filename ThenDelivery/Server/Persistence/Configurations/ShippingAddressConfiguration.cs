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

			builder.HasKey(e => e.Id);

			builder.Property(e => e.UserId)
					.HasColumnName("UserId")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.CityCode)
					.HasColumnName("CityCode")
					.HasMaxLength(2)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.DistrictCode)
					.HasColumnName("DistrictCode")
					.HasMaxLength(3)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.WardCode)
					.HasColumnName("WardCode")
					.HasMaxLength(5)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.HouseNumber)
					.HasColumnName("HouseNumber")
					.HasMaxLength(256)
					.IsRequired(true);
		}
	}
}
