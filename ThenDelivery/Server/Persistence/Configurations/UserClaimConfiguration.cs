using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
		{
			builder.ToTable("UserClaims");

			builder.Property(e => e.Id)
				.HasMaxLength(36);

			builder.Property(e => e.UserId)
				.HasMaxLength(36);
		}
	}
}
