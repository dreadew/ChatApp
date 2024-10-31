using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces.Validators;

public interface IChatValidator
{
  Task<ValidationResultModel> ValidateCreateRequestAsync(CreateChatRequest dto);
  Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateChatRequest dto);
  Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteChatRequest dto);
  Task<ValidationResultModel> ValidateFindOrCreatePrivateChatRequestAsync(FindOrCreatePrivateChatRequest dto);
}