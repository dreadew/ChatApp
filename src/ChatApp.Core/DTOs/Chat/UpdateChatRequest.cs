namespace ChatApp.Core.DTOs.Chat;

public class UpdateChatRequest
{
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public Guid CreatorId { get; set; }
  public List<Guid>? UsersIds { get; set; }
}