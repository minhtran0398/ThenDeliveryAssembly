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

			builder.HasKey(e => e.Id);

			builder.Property(e => e.OrderId)
					.IsRequired(true);

			builder.Property(e => e.ProductId)
					.IsRequired(true);

			builder.Property(e => e.Note)
					.HasMaxLength(256)
					.IsRequired(false);

			builder.Property(e => e.UnitPrice)
					.HasColumnType("decimal(8, 0)")
					.IsRequired(true);

			builder.Property(e => e.Quantity)
					.HasColumnType("smallint")
					.IsRequired(true);
		}
	}
}
