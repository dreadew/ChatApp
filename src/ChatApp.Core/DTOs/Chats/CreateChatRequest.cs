namespace ChatApp.Core.DTOs.Chats;

public class CreateChatRequest
{
  public string Name { get; set; } = string.Empty;
  public Guid CreatorId { get; set; }
  public bool IsGroupChat { get; set; }
  public List<Guid>? UsersIds { get; set; }
}