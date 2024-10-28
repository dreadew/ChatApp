namespace ChatApp.Application;

public class SendMessageRequest
{
  public Guid SenderId { get; set; }
  public Guid ChatId { get; set; }
  public required string Content { get; set; }
}