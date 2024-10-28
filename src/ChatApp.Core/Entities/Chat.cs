namespace ChatApp.Core.Entities;

public class Chat
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public bool IsGroupChat { get; set; }
  public ICollection<Message> Messages { get; set; }
  public ICollection<Guid> Users { get; set; }
}