using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Results;

namespace ChatApp.Core.Interfaces.Services;

public interface IMessageService
{
	Task<BaseResult<CreateMessageResponse>> CreateAsync(CreateMessageRequest dto);
	Task<BaseResult<MessageResponse>> GetByIdAsync(Guid messageId);
	Task<BaseResult<List<MessageResponse>>> ListByChatAsync(Guid chatId);
	Task<BaseResult> UpdateAsync(UpdateMessageRequest dto);
	Task<BaseResult> DeleteAsync(DeleteMessageRequest dto);
}