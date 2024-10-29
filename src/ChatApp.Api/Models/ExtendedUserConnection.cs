namespace ChatApp.Api.Models;

public record class ExtendedUserConnection(string username, Guid userId, Guid chatId);