namespace ChatApp.Core.Interfaces.Repositories;

public interface IBaseRepository
{
  Task<int> SaveChangesAsync();
}