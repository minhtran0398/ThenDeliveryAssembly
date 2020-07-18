using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
   public class CityLevelConfiguration : IEntityTypeConfiguration<CityLevel>
   {
      public void Configure(EntityTypeBuilder<CityLevel> builder)
      {
         builder.ToTable("CityLevels");

         builder.HasKey(e => e.CityLevelId);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.CityLevelId)
                .HasColumnName("CityLevelId")
                .HasColumnType("tinyint");

         builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasColumnType("nvarchar")
               .HasMaxLength(64)
               .IsRequired(true);
      }
   }
}
