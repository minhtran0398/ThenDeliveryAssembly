using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.ToTable("MenuItems");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.MerchantId)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasMaxLength(256)
					.IsRequired(false);
		}
	}
}
