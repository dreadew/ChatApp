namespace ChatApp.Application.Interfaces;

public interface IChatService
{
  Task SendMessageAsync(SendMessageRequest dto);
  Task UpdateMessageAsync(UpdateMessageRequest dto);
  Task<IEnumerable<MessageResponse>> ListMessagesAsync();
  Task DeleteMessageAsync(DeleteMessageRequest dto);
}