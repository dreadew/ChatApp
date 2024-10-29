using ChatApp.Core.Interfaces.Entities;

namespace ChatApp.Core.Entities;

public class Message : IEntityId<Guid>, IAuditableEntity
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public Guid ChatId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public Chat? Chat { get; set; }
}