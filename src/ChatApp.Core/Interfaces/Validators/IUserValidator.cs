using ChatApp.Core.DTOs.User;
using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces.Validators;

public interface IUserValidator
{
  Task<ValidationResultModel> ValidateCreateRequestAsync(CreateUserRequest dto);
  Task<ValidationResultModel> ValidateLoginRequestAsync(LoginUserRequest dto);
  Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateUserRequest dto);
  Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteUserRequest dto);
}