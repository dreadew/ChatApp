using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) {}
  public DbSet<Chat> Chats { get; set; }
  public DbSet<Message> Messages { get; set; }
}