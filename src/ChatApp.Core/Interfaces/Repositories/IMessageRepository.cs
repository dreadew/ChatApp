using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces.Repositories;

public interface IMessageRepository : IBaseRepository
{
  Task CreateAsync(Message message);
  Task<Message> GetByIdAsync(Guid messageId);
  Task<List<Message>> ListByChatAsync(Guid chatId);
  Message Update(Message message);
  void Delete(Message message);
}