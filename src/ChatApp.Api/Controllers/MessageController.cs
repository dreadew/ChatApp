using ChatApp.Api.Filters;
using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[TypeFilter(typeof(JwtAuthFilter))]
[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
	private readonly IMessageService _messageService;

	public MessageController(IMessageService messageService)
	{
		_messageService = messageService;
	}

	/// <summary>
  /// Обновляет сообщение.
  /// </summary>
  /// <param name="dto">Данные для обновления сообщения.</param>
  /// <returns>Результат операции.</returns>
  /// <response code="204">Сообщение успешно обновлено.</response>
  /// <response code="400">Если данные не валидны.</response>
  /// <response code="404">Если сообщение не найдено.</response>
  /// <response code="403">Если пользователь не имеет прав для обновления.</response>
	[HttpPatch]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(BaseResult), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResult), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(BaseResult), StatusCodes.Status403Forbidden)]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateMessageRequest dto)
	{
		var response = await _messageService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	/// <summary>
  /// Удаляет сообщение.
  /// </summary>
  /// <param name="dto">Данные для удаления сообщения.</param>
  /// <returns>Результат операции.</returns>
  /// <response code="204">Сообщение успешно удалено.</response>
  /// <response code="400">Если данные не валидны.</response>
  /// <response code="404">Если сообщение не найдено.</response>
	[HttpDelete]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(BaseResult), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResult), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BaseResult>> Delete([FromQuery] DeleteMessageRequest dto)
	{
		var response = await _messageService.DeleteAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}
}