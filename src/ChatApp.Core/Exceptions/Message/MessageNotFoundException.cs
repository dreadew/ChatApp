namespace ChatApp.Core.Exceptions.Message;

public class MessageNotFoundException : Exception
{
  public MessageNotFoundException(string message) : base(message) { }
}