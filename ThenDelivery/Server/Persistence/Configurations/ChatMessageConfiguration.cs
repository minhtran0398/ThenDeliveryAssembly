using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{
			builder.ToTable("ChatMessages");

			builder.HasKey(e => new { e.ChatId, e.SendTime });

			builder.Property(e => e.SendClientId)
					 .HasMaxLength(36)
					 .IsFixedLength();

			builder.Property(e => e.Text)
					.IsRequired(true);

			builder.Property(e => e.SendTime)
					.HasColumnType("datetime2")
					.IsRequired(true);
		}
	}
}
