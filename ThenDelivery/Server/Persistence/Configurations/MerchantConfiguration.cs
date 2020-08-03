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
					.HasColumnName("UserId")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Avatar)
					.HasColumnName("Avatar")
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);

			builder.Property(e => e.CoverPicture)
					.HasColumnName("CoverPicture")
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);

			builder.Property(e => e.TaxCode)
					.HasColumnName("TaxCode")
					.HasMaxLength(10)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.PhoneNumber)
					.HasColumnName("PhoneNumber")
					.HasMaxLength(16)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.OpenTime)
					.HasColumnName("OpenTime")
					.HasMaxLength(4)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.CloseTime)
					.HasColumnName("CloseTime")
					.HasMaxLength(4)
					.IsFixedLength()
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasColumnName("Description")
					.HasMaxLength(256)
					.IsRequired(true);

			builder.Property(e => e.SearchKey)
					.HasColumnName("SearchKey")
					.HasMaxLength(20)
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
