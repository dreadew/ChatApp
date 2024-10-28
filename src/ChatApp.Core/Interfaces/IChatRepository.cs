using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces;

public interface IChatRepository
{
  Task<Chat> GetChatByIdAsync(Guid chatId);
  Task<IEnumerable<Message>> ListMessagesByChatAsync(Guid chatId);
  Task CreateChatAsync(Chat chat);
  Task SendMessageAsync(Message message);
}