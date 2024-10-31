namespace ChatApp.Core.DTOs.Chat;

public class FindOrCreatePrivateChatRequest
{
  public List<Guid>? UsersIds { get; set; }
}