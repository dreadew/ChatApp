namespace ChatApp.Core.DTOs.User;

public class LoginUserResponse
{
  public string AccessToken { get; private set; } = string.Empty;
  public LoginUserResponse(string token)
  {
    AccessToken = token;
  }
}