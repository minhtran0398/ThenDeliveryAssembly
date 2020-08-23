using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using ThenDelivery.Shared.Common;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.ToTable("Roles");

			builder.Property(e => e.Id)
				.HasMaxLength(36);

			builder.Property(e => e.Name)
				.IsRequired(true);
		}
	}
}
