using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class MerTypeConfiguration : IEntityTypeConfiguration<MerType>
	{
		public void Configure(EntityTypeBuilder<MerType> builder)
		{
			builder.ToTable("MerTypes");

			builder.HasKey(e => e.Id);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Name)
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
