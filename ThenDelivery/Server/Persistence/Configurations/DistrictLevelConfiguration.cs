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

         builder.HasKey(e => e.DistrictLevelId);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.DistrictLevelId)
                .HasColumnName("DistrictLevelId")
                .HasColumnType("tinyint");

         builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasColumnType("nvarchar")
               .HasMaxLength(64)
               .IsRequired(true);
      }
	}
}
