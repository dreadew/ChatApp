namespace ChatApp.Core.DTOs.Messages;

public class CreateMessageRequest
{
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public Guid ChatId { get; set; }
}