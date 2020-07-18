using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class StoreMenuConfiguration : IEntityTypeConfiguration<StoreMenu>
	{
		public void Configure(EntityTypeBuilder<StoreMenu> builder)
		{
			builder.ToTable("StoreMenues");

			builder.HasKey(e => e.StoreMenuId);

			builder.Property(e => e.MerchantId)
					.HasColumnName("MerchantId")
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasColumnType("nvarchar")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasColumnName("Description")
					.HasColumnType("nvarchar")
					.HasMaxLength(256)
					.IsRequired(true);
		}
	}
}
