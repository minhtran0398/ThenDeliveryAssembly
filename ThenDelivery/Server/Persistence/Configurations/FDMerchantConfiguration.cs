using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class FDMerchantConfiguration : IEntityTypeConfiguration<FDMerchant>
	{
		public void Configure(EntityTypeBuilder<FDMerchant> builder)
		{
			builder.ToTable("FDMerchants");

			builder.HasKey(e => new { e.MerchantId, e.FeaturedDishId });
		}
	}
}
