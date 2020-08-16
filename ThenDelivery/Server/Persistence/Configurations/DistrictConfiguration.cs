using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class DistrictConfiguration : IEntityTypeConfiguration<District>
	{
		public void Configure(EntityTypeBuilder<District> builder)
		{
			builder.ToTable("Districts");

			builder.HasKey(e => e.DistrictCode);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.DistrictCode)
					 .HasMaxLength(3)
					 .IsFixedLength();

			builder.Property(e => e.CityCode)
					 .HasMaxLength(2)
					 .IsFixedLength()
					 .IsRequired(true);

			builder.Property(e => e.Name)
					.HasMaxLength(64)
					.IsRequired(true);

			builder.Property(e => e.DistrictLevelId)
					.HasColumnType("tinyint")
					.IsRequired(true);
		}
	}
}
