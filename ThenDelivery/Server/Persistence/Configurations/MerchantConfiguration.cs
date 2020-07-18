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

			builder.HasKey(e => e.MerchantId);

			builder.Property(e => e.UserId)
					.HasColumnName("UserId")
					.HasColumnType("nvarchar")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasColumnType("nvarchar")
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
					.HasColumnType("char")
					.HasMaxLength(10)
					.IsRequired(true);

			builder.Property(e => e.PhoneNumber)
					.HasColumnName("PhoneNumber")
					.HasColumnType("char")
					.HasMaxLength(16)
					.IsRequired(true);

			builder.Property(e => e.OpenTime)
					.HasColumnName("OpenTime")
					.HasColumnType("char")
					.HasMaxLength(4)
					.IsRequired(true);

			builder.Property(e => e.CloseTime)
					.HasColumnName("CloseTime")
					.HasColumnType("char")
					.HasMaxLength(4)
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasColumnName("Description")
					.HasColumnType("nvarchar")
					.HasMaxLength(256)
					.IsRequired(true);

			builder.Property(e => e.SearchKey)
					.HasColumnName("SearchKey")
					.HasColumnType("nvarchar")
					.HasMaxLength(20)
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
