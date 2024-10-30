namespace ChatApp.Core.DTOs.Chat;

public class CreateChatRequest
{
  public string Name { get; set; } = string.Empty;
  public bool IsGroupChat { get; set; }
  public List<Guid>? UsersIds { get; set; }
}