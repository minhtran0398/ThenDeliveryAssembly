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

			builder.HasData(new List<IdentityRole>
			{
				new IdentityRole(Const.Role.UserRole)
				{
					NormalizedName = Const.Role.UserRole.ToUpper()
				},
				new IdentityRole(Const.Role.ShipperRole)
				{
					NormalizedName = Const.Role.ShipperRole.ToUpper()
				},
				new IdentityRole(Const.Role.MerchantRole)
				{
					NormalizedName = Const.Role.MerchantRole.ToUpper()
				},
				new IdentityRole(Const.Role.AdministrationRole)
				{
					NormalizedName = Const.Role.AdministrationRole.ToUpper()
				},
			});
		}
	}
}
