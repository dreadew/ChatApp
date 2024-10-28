namespace ChatApp.Application;

public class MessageResponse
{
  public Guid Id { get; set; }
  public Guid SenderId { get; set; }
  public required string Content { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public Guid ChatId { get; set; }
}