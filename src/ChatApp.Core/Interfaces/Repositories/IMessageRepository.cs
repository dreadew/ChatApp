using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces.Repositories;

public interface IMessageRepository : IBaseRepository
{
  Task CreateAsync(Message message);
  Task<Message> GetByIdAsync(Guid messageId);
  Message Update(Message message);
  void Delete(Message message);
}