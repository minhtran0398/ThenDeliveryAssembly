using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class FeaturedDishCategoyConfiguration : IEntityTypeConfiguration<FeaturedDish>
	{
		public void Configure(EntityTypeBuilder<FeaturedDish> builder)
		{
			builder.ToTable("FeaturedDishCategoies");

			builder.HasKey(e => e.Id);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
