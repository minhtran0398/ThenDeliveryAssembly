using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.MerchantId)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.MenuItemId)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.IsAvailable)
					.HasColumnType("bit")
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasMaxLength(128)
					.IsRequired(false);

			builder.Property(e => e.OrderCount)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.FavoriteCount)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.UnitPrice)
					.HasColumnType("smallmoney")
					.IsRequired(true);

			builder.Property(e => e.Image)
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);
		}
	}
}
