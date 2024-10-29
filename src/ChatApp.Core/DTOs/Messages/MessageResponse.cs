namespace ChatApp.Core.DTOs.Messages;

public class MessageResponse
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public Guid ChatId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}