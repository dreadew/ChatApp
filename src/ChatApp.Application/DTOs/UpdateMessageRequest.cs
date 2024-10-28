namespace ChatApp.Application;

public class UpdateMessageRequest
{
  public Guid Id { get; set; }
  public Guid ChatId { get; set; }
  public required string Content { get; set; }
}