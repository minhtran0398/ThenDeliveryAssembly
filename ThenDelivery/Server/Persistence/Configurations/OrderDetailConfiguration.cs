using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.ToTable("OrderDetails");

			builder.HasKey(e => new { e.OrderId, e.ProductId });

			builder.Property(e => e.UnitPrice)
					.HasColumnName("UnitPrice")
					.HasColumnType("smallmoney")
					.IsRequired(true);

			builder.Property(e => e.Quantity)
					.HasColumnName("Quantity")
					.HasColumnType("smallint")
					.IsRequired(true);
		}
	}
}
