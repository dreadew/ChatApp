using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces.Validators;

public interface IMessageValidator
{
  Task<ValidationResultModel> ValidateCreateRequestAsync(CreateMessageRequest dto);
  Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateMessageRequest dto);
  Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteMessageRequest dto);
}