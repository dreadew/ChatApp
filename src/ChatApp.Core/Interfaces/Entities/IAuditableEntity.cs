namespace ChatApp.Core.Interfaces.Entities;

public interface IAuditableEntity
{
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}