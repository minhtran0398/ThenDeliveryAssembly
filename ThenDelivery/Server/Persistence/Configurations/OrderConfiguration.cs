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

			builder.HasKey(e => e.OrderId);

			builder.Property(e => e.UserId)
					.HasColumnName("UserId")
					.HasMaxLength(128)
					.IsRequired(true);

			builder.Property(e => e.ShipperId)
					.HasColumnName("ShipperId")
					.HasMaxLength(128)
					.IsRequired(false);

			builder.Property(e => e.OrderDateTime)
					.HasColumnName("OrderDateTime")
					.HasColumnType("datetime2")
					.IsRequired(true);

			builder.Property(e => e.Note)
					.HasColumnName("Note")
					.HasMaxLength(256)
					.IsRequired(false);

			builder.Property(e => e.ShippingAddressId)
					.HasColumnName("ShippingAddressId")
					.HasColumnType("int")
					.IsRequired(false);

			builder.Property(e => e.ReceiveVia)
					.HasColumnName("ReceiveVia")
					.HasColumnType("tinyint")
					.IsRequired(true);
		}
	}
}
