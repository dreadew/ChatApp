namespace ChatApp.Core.DTOs.Chats;

public class DeleteChatRequest
{
  public Guid Id { get; set; }
  public Guid CreatorId { get; set; }
}