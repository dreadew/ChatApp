using ChatApp.Core.Interfaces.Entities;

namespace ChatApp.Core.Entities;

public class Chat : IEntityId<Guid>, IAuditableEntity
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public bool IsGroupChat { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public ICollection<Message>? Messages { get; set; }
  public ICollection<User>? Users { get; set; }
}