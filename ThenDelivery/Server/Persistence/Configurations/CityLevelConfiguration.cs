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

         builder.HasKey(e => e.Id);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.Id)
                .HasColumnName("CityLevelId")
                .HasColumnType("tinyint");

         builder.Property(e => e.Name)
               .HasColumnName("Name")
               .HasMaxLength(64)
               .IsRequired(true);
      }
   }
}
