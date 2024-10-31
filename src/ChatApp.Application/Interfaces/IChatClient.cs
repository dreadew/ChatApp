namespace ChatApp.Application.Interfaces;

public interface IChatClient
{
	public Task ReceiveMessage(Guid userId, string username, Guid messageId, string message);
	public Task MessageDeleted(Guid messageId);
	public Task MessageUpdated(Guid messageId, string content);
}