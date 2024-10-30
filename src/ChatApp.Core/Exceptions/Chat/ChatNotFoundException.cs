namespace ChatApp.Core.Exceptions.Chat;

public class ChatNotFoundException : Exception
{
  public ChatNotFoundException(string message) : base(message) { }
}