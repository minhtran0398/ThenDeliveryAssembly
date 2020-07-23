using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class WardLevelConfiguration : IEntityTypeConfiguration<WardLevel>
	{
		public void Configure(EntityTypeBuilder<WardLevel> builder)
		{
         builder.ToTable("WardLevels");

         builder.HasKey(e => e.WardLevelId);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.WardLevelId)
                .HasColumnName("WardLevelId")
                .HasColumnType("tinyint");

         builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasMaxLength(64)
               .IsRequired(true);
      }
	}
}
