using ChatApp.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Api.Filters;

public class JwtAuthFilter : IAuthorizationFilter
{
	private const string AuthorizationHeader = "Authorization";
	private readonly IJwtProvider _jwtProvider;

	public JwtAuthFilter(IJwtProvider jwtProvider)
	{
		_jwtProvider = jwtProvider;
	}

	public void OnAuthorization(AuthorizationFilterContext context)
	{
		Console.WriteLine("++++");
		var token = context.HttpContext.Request.Headers[AuthorizationHeader].FirstOrDefault()?.Split(" ").Last();
		if (string.IsNullOrEmpty(token) || !_jwtProvider.ValidateToken(token))
		{
			context.Result = new UnauthorizedResult();
		}
	}
}