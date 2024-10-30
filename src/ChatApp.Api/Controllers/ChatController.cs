using ChatApp.Api.Filters;
using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[TypeFilter(typeof(JwtAuthFilter))]
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
	private readonly IChatService _chatService;

	public ChatController(IChatService chatService)
	{
		_chatService = chatService;
	}

	/// <summary>
  /// Создает новый чат.
  /// </summary>
  /// <param name="dto">Данные для создания чата.</param>
  /// <returns>Результат создания чата.</returns>
  [HttpPost]
  [ProducesResponseType(typeof(BaseResult<CreateChatResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<BaseResult<CreateChatResponse>>> Create([FromBody] CreateChatRequest dto)
	{
		var response = await _chatService.CreateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Возвращает чат по ID.
  /// </summary>
  /// <param name="id">ID чата.</param>
  /// <returns>Информация о чате.</returns>
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(BaseResult<ChatResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<BaseResult<ChatResponse>>> GetById(Guid id)
	{
		var response = await _chatService.GetByIdAsync(id);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Обновляет существующий чат.
  /// </summary>
  /// <param name="dto">Данные для обновления чата.</param>
  /// <returns>Результат обновления чата.</returns>
  [HttpPatch]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateChatRequest dto)
	{
		var response = await _chatService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Удаляет чат.
  /// </summary>
  /// <param name="dto">Запрос на удаление чата.</param>
  /// <returns>Результат удаления чата.</returns>
  [HttpDelete]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<BaseResult>> Delete([FromQuery] DeleteChatRequest dto)
	{
		var response = await _chatService.DeleteAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}
}