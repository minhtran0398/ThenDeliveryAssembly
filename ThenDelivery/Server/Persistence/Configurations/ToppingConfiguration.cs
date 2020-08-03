using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class ToppingConfiguration : IEntityTypeConfiguration<Topping>
	{
		public void Configure(EntityTypeBuilder<Topping> builder)
		{
			builder.ToTable("Topping");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
					.HasMaxLength(64)
					.IsRequired(true);

			builder.Property(e => e.ProductId)
					.HasColumnType("int")
					.IsRequired(true);

			builder.Property(e => e.UnitPrice)
					.HasColumnType("smallmoney")
					.IsRequired(true);
		}
	}
}
