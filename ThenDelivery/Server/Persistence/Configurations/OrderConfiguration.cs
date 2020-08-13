using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.UserId)
					.HasMaxLength(36)
					.IsRequired(true);

			builder.Property(e => e.ShipperId)
					.HasMaxLength(36)
					.IsRequired(false);

			builder.Property(e => e.OrderDateTime)
					.HasColumnType("datetime2")
					.IsRequired(true);

			builder.Property(e => e.Note)
					.HasMaxLength(256)
					.IsRequired(false);

			builder.Property(e => e.ShippingAddressId)
					.HasColumnType("int")
					.IsRequired(false);

			builder.Property(e => e.ReceiveVia)
					.HasColumnType("tinyint")
					.IsRequired(true);

			builder.Property(e => e.DeliveryDateTime)
					.HasColumnType("datetime2")
					.IsRequired(true);
		}
	}
}
