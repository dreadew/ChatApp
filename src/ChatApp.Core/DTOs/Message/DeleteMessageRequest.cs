namespace ChatApp.Core.DTOs.Message;

public class DeleteMessageRequest
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
}