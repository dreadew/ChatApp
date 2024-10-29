namespace ChatApp.Api.Interfaces;

public interface IChatClient
{
	public Task ReceiveMessage(string username, string message);
}