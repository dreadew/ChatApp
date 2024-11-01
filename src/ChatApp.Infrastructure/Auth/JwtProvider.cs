using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Core.Entities;

namespace ChatApp.Infrastructure.Auth;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
  private readonly JwtOptions _options = options.Value;

  public string GenerateToken(User user)
  {
    Claim[] claims = [new("userId", user.Id.ToString())];

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
      SecurityAlgorithms.HmacSha256
    );

    var token = new JwtSecurityToken(
      claims: claims,
      signingCredentials: signingCredentials,
      expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
    );

    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

    return tokenValue;
  }

  public bool ValidateToken(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var validationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
      ValidateIssuer = false,
      ValidateAudience = false,
      ClockSkew = TimeSpan.Zero
    };

    try
    {
      tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
      return validatedToken is JwtSecurityToken;
    }
    catch
    {
      return false;
    }
  }
}