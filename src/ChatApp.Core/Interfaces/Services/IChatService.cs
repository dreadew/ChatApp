using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Results;

namespace ChatApp.Core.Interfaces.Services;

public interface IChatService
{
  Task<BaseResult<CreateChatResponse>> CreateAsync(CreateChatRequest dto);
  Task<BaseResult<ChatResponse>> GetByIdAsync(Guid chatId);
  Task<BaseResult> UpdateAsync(UpdateChatRequest dto);
  Task<BaseResult> DeleteAsync(DeleteChatRequest dto);
}