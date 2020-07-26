using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerchantTypeConfiguration : IEntityTypeConfiguration<MerchantType>
	{
		public void Configure(EntityTypeBuilder<MerchantType> builder)
		{
			builder.ToTable("MerchantTypes");

			builder.HasKey(e => e.MerchantTypeId);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
