using ChatApp.Api.Filters;
using ChatApp.Core.DTOs.User;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	/// <summary>
  /// Создает нового пользователя.
  /// </summary>
  /// <param name="dto">Данные для создания пользователя.</param>
  /// <returns>Информация о созданном пользователе.</returns>
  /// <response code="200">Пользователь успешно создан.</response>
  /// <response code="400">Ошибка валидации данных.</response>
	[HttpPost("create")]
  [ProducesResponseType(typeof(BaseResult<CreateUserResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<BaseResult<CreateUserResponse>>> Create([FromBody] CreateUserRequest dto)
	{
		var response = await _userService.CreateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Аутентификация пользователя.
  /// </summary>
  /// <param name="dto">Данные для входа.</param>
  /// <returns>Токен доступа.</returns>
  /// <response code="200">Пользователь успешно вошел в систему.</response>
  /// <response code="400">Неправильное имя пользователя или пароль.</response>
	[HttpPost("login")]
	[ProducesResponseType(typeof(BaseResult<LoginUserResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<BaseResult<LoginUserResponse>>> Login([FromBody] LoginUserRequest dto)
	{
		var response = await _userService.LoginAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Возвращает информацию о пользователе по идентификатору.
  /// </summary>
  /// <param name="id">Идентификатор пользователя.</param>
  /// <returns>Информация о пользователе.</returns>
  /// <response code="200">Информация о пользователе.</response>
  /// <response code="404">Пользователь не найден.</response>
	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(BaseResult<UserResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BaseResult<UserResponse>>> GetById(Guid id)
	{
		var response = await _userService.GetByIdAsync(id);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Возвращает список пользователей.
  /// </summary>
  /// <param name="dto">Параметры для получения списка пользователей.</param>
  /// <returns>Список пользователей.</returns>
  /// <response code="200">Список пользователей.</response>
  /// <response code="404">Пользователи не найдены.</response>
	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpGet("list")]
	[ProducesResponseType(typeof(BaseResult<List<UserResponse>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BaseResult<List<UserResponse>>>> List([FromQuery] ListUserRequest dto)
	{
		var response = await _userService.ListAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Обновляет информацию о пользователе.
  /// </summary>
  /// <param name="dto">Данные для обновления пользователя.</param>
  /// <returns>Статус операции.</returns>
  /// <response code="200">Пользователь успешно обновлен.</response>
  /// <response code="400">Ошибка валидации данных.</response>
  /// <response code="404">Пользователь не найден.</response>
	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpPatch]
	[ProducesResponseType(typeof(BaseResult), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateUserRequest dto)
	{
		var response = await _userService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Удаляет пользователя.
  /// </summary>
  /// <param name="dto">Данные для удаления пользователя.</param>
  /// <returns>Статус операции.</returns>
  /// <response code="200">Пользователь успешно удален.</response>
  /// <response code="400">Ошибка валидации данных.</response>
  /// <response code="404">Пользователь не найден.</response>
	[TypeFilter(typeof(JwtAuthFilter))]
	[HttpDelete]
	[ProducesResponseType(typeof(BaseResult), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
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