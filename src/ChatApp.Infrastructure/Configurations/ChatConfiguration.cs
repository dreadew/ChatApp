using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
  const string UserChatsTable = "UserChats";
  public void Configure(EntityTypeBuilder<Chat> builder)
  {
    builder.Property(c => c.Id).ValueGeneratedOnAdd();

    // Настройка связи между Chat и User
    builder.HasMany(c => c.Users)
      .WithMany(c => c.Chats)
      .UsingEntity(j => j.ToTable(UserChatsTable));

    // Настройка связи между Chat и Message
    builder.HasMany(c => c.Messages)
      .WithOne(m => m.Chat)
      .HasForeignKey(m => m.ChatId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}