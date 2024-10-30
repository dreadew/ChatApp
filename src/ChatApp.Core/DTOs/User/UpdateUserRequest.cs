namespace ChatApp.Core.DTOs.User;

public class UpdateUserRequest
{
  public Guid Id { get; set; }
  public string? Password { get; set; } = string.Empty;
}