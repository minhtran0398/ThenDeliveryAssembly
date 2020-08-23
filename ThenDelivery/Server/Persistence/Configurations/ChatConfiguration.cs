using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class ChatConfiguration : IEntityTypeConfiguration<Chat>
	{
		public void Configure(EntityTypeBuilder<Chat> builder)
		{
			builder.ToTable("Chats");

			builder.HasKey(e => new { e.Id, e.Client1Id, e.Client2Id });

			builder.Property(e => e.Id)
					.HasColumnType("int");

			builder.Property(e => e.Client1Id)
					.HasMaxLength(36);

			builder.Property(e => e.Client2Id)
					.HasMaxLength(36);

			builder.Property(e => e.CreateAt)
					.HasColumnType("datetime2")
					.IsRequired(true);
		}
	}
}
