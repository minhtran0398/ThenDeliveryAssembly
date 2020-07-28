using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantMenuConfiguration : IEntityTypeConfiguration<MerchantMenu>
	{
		public void Configure(EntityTypeBuilder<MerchantMenu> builder)
		{
			builder.ToTable("MerchantMenues");

			builder.HasKey(e => e.MerchantMenuId);

			builder.Property(e => e.MerchantId)
					.HasColumnName("MerchantId")
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasColumnName("Description")
					.HasMaxLength(256)
					.IsRequired(true);
		}
	}
}
