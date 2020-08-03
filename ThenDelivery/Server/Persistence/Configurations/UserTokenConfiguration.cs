using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
		{
			builder.ToTable("UserTokens");

			builder.Property(e => e.UserId)
				.HasMaxLength(36);
		}
	}
}
