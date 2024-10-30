namespace ChatApp.Core.Exceptions.User;

public class UserAlreadyExistedException : Exception
{
  public UserAlreadyExistedException(string message) : base(message) { }
}