using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantTypeMerchantConfiguration : IEntityTypeConfiguration<MTMerchant>
	{
		public void Configure(EntityTypeBuilder<MTMerchant> builder)
		{
			builder.ToTable("MerchantTypeMerchants");

			builder.HasKey(e => new { e.MerchantId, e.MerchantTypeId });
		}
	}
}
