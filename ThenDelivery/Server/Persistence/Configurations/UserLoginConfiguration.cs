using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
		{
			builder.ToTable("UserLogins");

			builder.Property(e => e.UserId)
				.HasMaxLength(36);
		}
	}
}
