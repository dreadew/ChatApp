namespace ChatApp.Core.Exceptions.User;

public class UserNotFoundException : Exception
{
  public UserNotFoundException(string message) : base(message) { }
}