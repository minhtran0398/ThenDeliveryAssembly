using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class OrderDetailToppingConfiguration : IEntityTypeConfiguration<OrderDetailTopping>
	{
		public void Configure(EntityTypeBuilder<OrderDetailTopping> builder)
		{
			builder.ToTable("OrderDetailToppings");

			builder.HasKey(e => new { e.OrderDetailId, e.ToppingId });
		}
	}
}
