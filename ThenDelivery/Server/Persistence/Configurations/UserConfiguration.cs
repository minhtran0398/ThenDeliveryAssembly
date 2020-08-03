using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.UserName)
				.IsRequired(true);

			builder.Property(e => e.Email)
				.IsRequired(true);

			builder.Property(e => e.BirthDate)
					.HasColumnType("datetime2");

			builder.Property(e => e.PhoneNumber)
				.HasMaxLength(10)
				.IsFixedLength();

			builder.Property(e => e.Id)
				.HasMaxLength(36);
		}
	}
}
