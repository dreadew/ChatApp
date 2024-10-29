namespace ChatApp.Core.DTOs.Chats;

public class UpdateChatRequest
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public Guid CreatorId { get; set; }
}