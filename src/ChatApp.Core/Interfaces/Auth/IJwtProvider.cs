using ChatApp.Core.Entities;

namespace ChatApp.Core.Interfaces.Auth;

public interface IJwtProvider
{
  string GenerateToken(User user);
}