using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantTypeMerchantConfiguration : IEntityTypeConfiguration<MerchantTypeMerchant>
	{
		public void Configure(EntityTypeBuilder<MerchantTypeMerchant> builder)
		{
			builder.ToTable("MerchantTypeMerchants");

			builder.HasKey(e => new { e.MerchantId, e.MerchantTypeId });
		}
	}
}
