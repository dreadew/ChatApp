namespace ChatApp.Core.Exceptions.Message;

public class MessageAlreadyExistedException : Exception
{
  public MessageAlreadyExistedException(string message) : base(message) { }
}