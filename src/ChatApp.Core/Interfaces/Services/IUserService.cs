using ChatApp.Core.DTOs.Users;
using ChatApp.Core.Results;

namespace ChatApp.Core.Interfaces.Services;

public interface IUserService
{
  Task<BaseResult<CreateUserResponse>> CreateAsync(CreateUserRequest dto);
  Task<BaseResult<UserResponse>> GetByIdAsync(Guid userId);
  Task<BaseResult> UpdateAsync(UpdateUserRequest dto);
  Task DeleteAsync(DeleteUserRequest dto);
}