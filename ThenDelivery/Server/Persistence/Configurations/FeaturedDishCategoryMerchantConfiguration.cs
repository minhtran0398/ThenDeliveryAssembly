using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class FeaturedDishCategoryMerchantConfiguration : IEntityTypeConfiguration<FDMerchant>
	{
		public void Configure(EntityTypeBuilder<FDMerchant> builder)
		{
			builder.ToTable("FeaturedDishCategoryMerchants");

			builder.HasKey(e => new { e.MerchantId, e.FeaturedDishId });
		}
	}
}
