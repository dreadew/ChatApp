namespace ChatApp.Core.Exceptions.Chat;

public class ChatAlreadyExistedException : Exception
{
  public ChatAlreadyExistedException(string message) : base(message) { }
}