using ChatApp.Api.Filters;
using ChatApp.Core.DTOs.Users;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("/create")]
	public async Task<ActionResult<BaseResult<CreateUserResponse>>> Create([FromBody] CreateUserRequest dto)
	{
		var response = await _userService.CreateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[HttpPost("/login")]
	public async Task<ActionResult<BaseResult<LoginUserResponse>>> Login([FromBody] LoginUserRequest dto)
	{
		var response = await _userService.LoginAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpGet("{id}")]
	public async Task<ActionResult<BaseResult<UserResponse>>> GetById(Guid id)
	{
		var response = await _userService.GetByIdAsync(id);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpPatch]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateUserRequest dto)
	{
		var response = await _userService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpDelete]
	public async Task<ActionResult<BaseResult>> Delete([FromQuery] DeleteUserRequest dto)
	{
		var response = await _userService.DeleteAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}
}