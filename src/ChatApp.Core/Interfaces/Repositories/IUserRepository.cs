using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository
{
  Task CreateAsync(User user);
  Task<User> GetByIdAsync(Guid userId);
  Task<User> GetByUsernameAsync(string username);
  Task<List<User>> GetByIdsAsync(List<Guid> userIds);
  Task<List<User>> ListAsync();
  User Update(User user);
  void Delete(User user);
}