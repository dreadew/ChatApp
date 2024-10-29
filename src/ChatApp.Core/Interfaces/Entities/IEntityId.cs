namespace ChatApp.Core.Interfaces.Entities;

public interface IEntityId<T>
{
  T Id { get; set; }
}