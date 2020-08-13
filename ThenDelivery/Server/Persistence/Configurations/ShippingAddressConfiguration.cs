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
					.HasMaxLength(36)
					.IsRequired(true);

			builder.Property(e => e.CityCode)
					.HasMaxLength(2)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.DistrictCode)
					.HasMaxLength(3)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.WardCode)
					.HasMaxLength(5)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.HouseNumber)
					.HasMaxLength(256)
					.IsRequired(true);

			builder.Property(e => e.FullName)
					.HasMaxLength(256)
					.IsRequired(true);

			builder.Property(e => e.PhoneNumber)
					.HasColumnType("nchar(10)")
					.IsRequired(true);
		}
	}
}
