namespace ChatApp.Application;

public class DeleteMessageRequest
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
}