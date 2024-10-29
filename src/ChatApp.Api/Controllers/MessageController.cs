using ChatApp.Core.DTOs.Messages;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
	private readonly IMessageService _messageService;

	public MessageController(IMessageService messageService)
	{
		_messageService = messageService;
	}

	[HttpPatch]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateMessageRequest dto)
	{
		var response = await _messageService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[HttpDelete]
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