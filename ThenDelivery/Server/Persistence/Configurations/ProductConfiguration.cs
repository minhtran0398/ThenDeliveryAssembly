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

			builder.HasKey(e => e.ProductId);

			builder.Property(e => e.MerchantMenuId)
					.HasColumnName("MerchantMenuId")
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasColumnType("nvarchar")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.IsAvailable)
					.HasColumnName("IsAvailable")
					.HasColumnType("bit")
					.IsRequired(true);

			builder.Property(e => e.Description)
					.HasColumnName("Description")
					.HasColumnType("nvarchar")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.OrderCount)
					.HasColumnName("OrderCount")
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.FavoriteCount)
					.HasColumnName("FavoriteCount")
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.UnitPrice)
					.HasColumnName("UnitPrice")
					.HasColumnType("smallmoney")
					.IsRequired(true);

			builder.Property(e => e.Image)
					.HasColumnName("Image")
					.HasColumnType("nvarchar(MAX)")
					.IsRequired(true);
		}
	}
}
