using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class DistrictLevelConfiguration : IEntityTypeConfiguration<DistrictLevel>
	{
		public void Configure(EntityTypeBuilder<DistrictLevel> builder)
		{
			builder.ToTable("DistrictLevels");

			builder.HasKey(e => e.Id);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Id)
					 .HasColumnType("tinyint");

			builder.Property(e => e.Name)
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
