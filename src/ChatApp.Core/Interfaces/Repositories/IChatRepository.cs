using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces.Repositories;

public interface IChatRepository : IBaseRepository
{
  Task CreateAsync(Chat chat);
  Task AppendUsersAsync(Chat chat, List<User> users);
  Task RemoveUsersAsync(Chat chat, List<User> users);
  Task<Chat> GetByIdAsync(Guid chatId);
  Task<List<Chat>> ListChatsByUserAsync(Guid userId);
  Chat Update(Chat chat);
  void Delete(Chat chat);
}