using ChatApp.Core.DTOs.Users;

namespace ChatApp.Core.DTOs.Chats;

public class ChatResponse
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public bool IsGroupChat { get; set; }
  public List<UserResponse> Users { get; set; } = [];
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}