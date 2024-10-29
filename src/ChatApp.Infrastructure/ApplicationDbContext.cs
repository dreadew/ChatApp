using ChatApp.Core.Entities;
using ChatApp.Infrastructure.Configurations;
using ChatApp.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) {}
  public DbSet<Chat> Chats { get; set; }
  public DbSet<Message> Messages { get; set; }
  public DbSet<User> Users { get; set; }
  
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.AddInterceptors(new DateInterceptor());
  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
    modelBuilder.ApplyConfiguration(new ChatConfiguration());
    modelBuilder.ApplyConfiguration(new MessageConfiguration());
  }
}