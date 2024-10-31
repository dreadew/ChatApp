using System.Text.Json;
using ChatApp.Application.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatApp.Api.Hubs;

internal class ChatHub : Hub<IChatClient>
{
	private readonly IChatService _chatService;
	private readonly IMessageService _messageService;
	private readonly IDistributedCache _cache;

	public ChatHub(IChatService chatService, IMessageService messageService, IDistributedCache cache)
	{
		_chatService = chatService;
		_messageService = messageService;
		_cache = cache;
	}

	public async Task JoinChat(UserConnection connection)
	{
		var isExistingChat = await _chatService.GetByIdAsync(connection.chatId);
		if (!isExistingChat.IsSuccess || isExistingChat.Data == null)
		{
			return;
		}

		string? username = isExistingChat.Data
			.Users.Where(u => u.Id == connection.userId)
			.Select(u => u.Username).FirstOrDefault();

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
				var fromUser = isExistingChat.Data?.Users
					.Where(u => u.Id == message.SenderId)
					.FirstOrDefault();
				await Clients.Caller
					.ReceiveMessage(message.SenderId, fromUser?.Username ?? message.SenderId.ToString(), message.Id, message.Content);
			}
		}

		await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
		await Clients
			.Group(chatId)
			.ReceiveMessage(Guid.Empty, "System", Guid.Empty, $"Пользователь {username} присоединился к чату");
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

			var res = await _messageService.CreateAsync(dto);

			await Clients
				.Group(dto.ChatId.ToString())
				.ReceiveMessage(connection.userId, connection.username, res.Data!.Id, message);
		}
	}

	public async Task UpdateMessage(Guid messageId, string content)
	{
		var stringConnection = await _cache.GetAsync(Context.ConnectionId);

		var connection = JsonSerializer.Deserialize<ExtendedUserConnection>(stringConnection);

    if (connection is not null)
		{
			var dto = new UpdateMessageRequest {
				Id = messageId,
				Content = content,
				SenderId = connection.userId
			};

			await _messageService.UpdateAsync(dto);

			await Clients.Group(connection.chatId.ToString())
				.MessageUpdated(messageId, content);

			/*if (response.IsSuccess)
			{
				await Clients.Group(connection.chatId.ToString())
					.ReceiveMessage(Guid.Empty, "System", Guid.Empty, $"Сообщение с ID {dto.Id} было удалено.");
			}*/
		}
	}

	public async Task DeleteMessage(Guid messageId)
	{
		var stringConnection = await _cache.GetAsync(Context.ConnectionId);

		var connection = JsonSerializer.Deserialize<ExtendedUserConnection>(stringConnection);

    if (connection is not null)
		{
			var dto = new DeleteMessageRequest {
				Id = messageId,
				SenderId = connection.userId
			};

			await _messageService.DeleteAsync(dto);

			await Clients.Group(connection.chatId.ToString())
				.MessageDeleted(messageId);

			/*if (response.IsSuccess)
			{
				await Clients.Group(connection.chatId.ToString())
					.ReceiveMessage(Guid.Empty, "System", Guid.Empty, $"Сообщение с ID {dto.Id} было удалено.");
			}*/
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
				.ReceiveMessage(Guid.Empty, "System", Guid.Empty, $"Пользователь {connection.username} вышел из чата");
		}

		await base.OnDisconnectedAsync(exception);
	}
}