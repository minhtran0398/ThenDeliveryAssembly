using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class WardConfiguration : IEntityTypeConfiguration<Ward>
	{
		public void Configure(EntityTypeBuilder<Ward> builder)
		{
         builder.ToTable("Wards");

         builder.HasKey(e => e.WardCode);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.WardCode)
                .HasMaxLength(5)
                .IsFixedLength();

         builder.Property(e => e.DistrictCode)
                .HasMaxLength(3)
                .IsFixedLength();

         builder.Property(e => e.Name)
               .HasMaxLength(64)
               .IsRequired(true);

         builder.Property(e => e.WardLevelId)
               .HasColumnType("tinyint")
               .IsRequired(true);
      }
	}
}
