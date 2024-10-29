using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
  public void Configure(EntityTypeBuilder<Message> builder)
  {
    builder.Property(m => m.Id).ValueGeneratedOnAdd();
    builder.Property(m => m.Content).IsRequired();
    builder.Property(m => m.ChatId).IsRequired();
    builder.Property(m => m.SenderId).IsRequired();

    // Настройка связи между Message и Chat
    builder.HasOne(m => m.Chat)
      .WithMany(c => c.Messages)
      .HasForeignKey(m => m.ChatId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}