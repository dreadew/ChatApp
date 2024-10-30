using ChatApp.Core.DTOs.User;
using ChatApp.Core.Results;

namespace ChatApp.Core.Interfaces.Services;

public interface IUserService
{
  Task<BaseResult<CreateUserResponse>> CreateAsync(CreateUserRequest dto);
  Task<BaseResult<LoginUserResponse>> LoginAsync(LoginUserRequest dto);
  Task<BaseResult<UserResponse>> GetByIdAsync(Guid userId);
  Task<BaseResult<List<UserResponse>>> ListAsync(ListUserRequest dto);
  Task<BaseResult> UpdateAsync(UpdateUserRequest dto);
  Task<BaseResult> DeleteAsync(DeleteUserRequest dto);
}