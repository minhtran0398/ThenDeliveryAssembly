using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class FeaturedDishCategoyConfiguration : IEntityTypeConfiguration<FeaturedDishCategoy>
	{
		public void Configure(EntityTypeBuilder<FeaturedDishCategoy> builder)
		{
			builder.ToTable("FeaturedDishCategoies");

			builder.HasKey(e => e.FeaturedDishCategoryId);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasColumnType("nvarchar")
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
