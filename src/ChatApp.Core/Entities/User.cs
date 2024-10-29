using ChatApp.Core.Interfaces.Entities;

namespace ChatApp.Core.Entities;

public class User : IEntityId<Guid>, IAuditableEntity
{
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public ICollection<Chat>? Chats { get; set; }
}