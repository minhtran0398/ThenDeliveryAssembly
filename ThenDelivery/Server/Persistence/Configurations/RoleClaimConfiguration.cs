using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
		{
			builder.ToTable("RoleClaims");

			builder.Property(e => e.Id)
				.HasMaxLength(36);
		}
	}
}
