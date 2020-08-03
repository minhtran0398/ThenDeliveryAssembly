using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantMenuConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.ToTable("MerchantMenues");

			builder.HasKey(e => e.Id);

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
