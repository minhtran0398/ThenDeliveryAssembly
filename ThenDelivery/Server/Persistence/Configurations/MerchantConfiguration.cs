using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
	{
		public void Configure(EntityTypeBuilder<Merchant> builder)
		{
			builder.ToTable("Merchants");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.UserId)
					.HasMaxLength(36)
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Avatar)
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);

			builder.Property(e => e.CoverPicture)
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);

			builder.Property(e => e.TaxCode)
					.HasMaxLength(10)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.PhoneNumber)
					.HasMaxLength(16)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.OpenTime)
					.HasMaxLength(4)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.CloseTime)
					.HasMaxLength(4)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasMaxLength(256)
					.IsRequired(false);

			builder.Property(e => e.SearchKey)
					.HasMaxLength(20)
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
		}
	}
}
