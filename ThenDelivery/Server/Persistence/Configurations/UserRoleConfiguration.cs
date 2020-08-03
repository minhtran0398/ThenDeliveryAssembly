using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.ToTable("UserRoles");

			builder.Property(e => e.UserId)
				.HasMaxLength(36);

			builder.Property(e => e.RoleId)
				.HasMaxLength(36);
		}
	}
}
