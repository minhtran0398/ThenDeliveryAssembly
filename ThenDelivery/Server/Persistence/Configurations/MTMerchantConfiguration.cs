using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MTMerchantConfiguration : IEntityTypeConfiguration<MTMerchant>
	{
		public void Configure(EntityTypeBuilder<MTMerchant> builder)
		{
			builder.ToTable("MTMerchants");

			builder.HasKey(e => new { e.MerchantId, e.MerchantTypeId });
		}
	}
}
