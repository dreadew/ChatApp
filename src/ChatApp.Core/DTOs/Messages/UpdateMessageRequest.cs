namespace ChatApp.Core.DTOs.Messages;

public class UpdateMessageRequest
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
}