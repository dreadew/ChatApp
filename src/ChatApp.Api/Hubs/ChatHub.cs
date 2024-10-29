using System.Text.Json;
using ChatApp.Application.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Core.DTOs.Messages;
using ChatApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using ChatApp.Core.Interfaces.Auth;

namespace ChatApp.Api.Hubs;

internal class ChatHub : Hub<IChatClient>
{
	private const string AuthorizationHeader = "Authorization";
	private readonly IChatService _chatService;
	private readonly IMessageService _messageService;
	private readonly IDistributedCache _cache;
	private readonly IJwtProvider _jwtProvider;

	public ChatHub(IChatService chatService, IMessageService messageService, IDistributedCache cache, IJwtProvider jwtProvider)
	{
		_chatService = chatService;
		_messageService = messageService;
		_cache = cache;
		_jwtProvider = jwtProvider;
	}
	public async Task JoinChat(UserConnection connection)
	{
		var isExistingChat = await _chatService.GetByIdAsync(connection.chatId);
		if (!isExistingChat.IsSuccess || isExistingChat.Data == null)
		{
			return;
		}

		string? username = isExistingChat.Data.Users.Where(u => u.Id == connection.userId).Select(u => u.Username).FirstOrDefault();

		if (username == null)
		{
			return;
		}

		var extendedConnection = new ExtendedUserConnection(
			username,
			connection.userId,
			connection.chatId
		);
		var stringConnection = JsonSerializer.Serialize(extendedConnection);
		await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

		string chatId = connection.chatId.ToString();
		var messages = await _messageService.ListByChatAsync(connection.chatId);

		if (messages.IsSuccess && messages.Data != null)
		{
			foreach (var message in messages.Data)
			{
				await Clients.Caller.ReceiveMessage(message.SenderId.ToString(), message.Content);
			}
		}

		await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
		await Clients
			.Group(chatId)
			.ReceiveMessage("System", $"Пользователь {username} присоединился к чату");
	}

	public async Task SendMessage(string message)
	{
		var stringConnection = await _cache.GetAsync(Context.ConnectionId);

		var connection = JsonSerializer.Deserialize<ExtendedUserConnection>(stringConnection);

		if (connection is not null)
		{
			var dto = new CreateMessageRequest {
				SenderId = connection.userId,
				Content = message,
				ChatId = connection.chatId
			};

			await _messageService.CreateAsync(dto);

			await Clients
				.Group(dto.ChatId.ToString())
				.ReceiveMessage(connection.username, message);
		}
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var stringConnection = await _cache.GetAsync(Context.ConnectionId);

		var connection = JsonSerializer.Deserialize<ExtendedUserConnection>(stringConnection);

		if (connection is not null)
		{
			string chatId = connection.chatId.ToString();
			await _cache.RemoveAsync(Context.ConnectionId);
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);

			await Clients
				.Group(chatId)
				.ReceiveMessage("System", $"Пользователь {connection.username} вышел из чата");
		}

		await base.OnDisconnectedAsync(exception);
	}

	public override async Task OnConnectedAsync()
	{
		var token = Context.GetHttpContext()?.Request.Headers[AuthorizationHeader].FirstOrDefault()?.Split(" ").Last();
		if (string.IsNullOrEmpty(token) || !_jwtProvider.ValidateToken(token))
		{
			Context.Abort();
			return;
		}

		await base.OnConnectedAsync();
	}
}