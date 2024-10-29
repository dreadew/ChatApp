using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  const string UserChatsTable = "UserChats";
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(m => m.Id).ValueGeneratedOnAdd();
    builder.Property(m => m.Username).IsRequired();
    builder.Property(m => m.Email).IsRequired();

    builder.HasIndex(u => u.Username).IsUnique();
    builder.HasIndex(u => u.Email).IsUnique();

    // Настройка связи между User и Chat
    builder.HasMany(u => u.Chats)
      .WithMany(c => c.Users)
      .UsingEntity(j => j.ToTable(UserChatsTable));
  }
}