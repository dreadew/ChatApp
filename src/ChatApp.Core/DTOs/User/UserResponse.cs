using ChatApp.Core.DTOs.Chat;

namespace ChatApp.Core.DTOs.User;

public class UserResponse
{
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public ICollection<ChatResponseWithoutUsers>? Chats { get; set; }
}