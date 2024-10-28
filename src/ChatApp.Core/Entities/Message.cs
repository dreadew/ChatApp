namespace ChatApp.Core.Entities;

public class Message
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public Guid ChatId { get; set; }
  public required Chat Chat { get; set; }
}