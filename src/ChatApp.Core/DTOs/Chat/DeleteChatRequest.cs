namespace ChatApp.Core.DTOs.Chat;

public class DeleteChatRequest
{
  public Guid Id { get; set; }
  public Guid CreatorId { get; set; }
}