using ChatApp.Core.DTOs.Chats;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
	private readonly IChatService _chatService;

	public ChatController(IChatService chatService)
	{
		_chatService = chatService;
	}

	[HttpPost]
	public async Task<ActionResult<BaseResult<CreateChatResponse>>> Create([FromBody] CreateChatRequest dto)
	{
		var response = await _chatService.CreateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<BaseResult<ChatResponse>>> GetById(Guid id)
	{
		var response = await _chatService.GetByIdAsync(id);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[HttpPatch]
	public async Task<ActionResult<BaseResult>> Update([FromBody] UpdateChatRequest dto)
	{
		var response = await _chatService.UpdateAsync(dto);

		if (response.IsSuccess)
		{
			return Ok(response);
		}

		return StatusCode(response.ErrorCode, response.ErrorMessage);
	}

	[HttpDelete]
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