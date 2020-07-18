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
                .HasColumnName("DistrictCode")
                .HasColumnType("char")
                .HasMaxLength(3);

         builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasColumnType("nvarchar")
               .HasMaxLength(64)
               .IsRequired(true);

         builder.Property(e => e.DistrictLevelId)
               .HasColumnName("DistrictLevelId")
               .HasColumnType("tinyint")
               .IsRequired(true);
      }
   }
}
