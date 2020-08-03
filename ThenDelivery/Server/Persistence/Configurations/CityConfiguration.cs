using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
   public class CityConfiguration : IEntityTypeConfiguration<City>
   {
      public void Configure(EntityTypeBuilder<City> builder)
      {
         builder.ToTable("Cities");

         builder.HasKey(e => e.CityCode);
         builder.HasIndex(e => e.Name).IsUnique(true);

         builder.Property(e => e.CityCode)
                .HasMaxLength(2)
                .IsFixedLength();

         builder.Property(e => e.Name)
               .HasMaxLength(64)
               .IsRequired(true);

         builder.Property(e => e.CityLevelId)
               .HasColumnType("tinyint")
               .IsRequired(true);
      }
   }
}
