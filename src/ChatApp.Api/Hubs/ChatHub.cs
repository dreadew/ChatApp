using ChatApp.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs;

internal class ChatHub : Hub
{
  private readonly IChatService _chatService;
  public ChatHub(IChatService chatService)
  {
    _chatService = chatService;
  }
}