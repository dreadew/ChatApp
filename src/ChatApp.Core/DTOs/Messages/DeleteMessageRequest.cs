namespace ChatApp.Core.DTOs.Messages;

public class DeleteMessageRequest
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
}