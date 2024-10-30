namespace ChatApp.Core.DTOs.Message;

public class CreateMessageRequest
{
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public Guid ChatId { get; set; }
}