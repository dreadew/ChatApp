using System.Net;
using AutoMapper;
using ChatApp.Core.DTOs.Users;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using Serilog;
using ChatApp.Core.Interfaces.Validators;
using Microsoft.Extensions.Logging;

namespace ChatApp.Application.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepo;
  private readonly ILogger<UserService> _logger;
  private readonly IMapper _mapper;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtProvider _jwtProvider;
  private readonly IUserValidator _userValidator;

  public UserService(IUserRepository userRepo, ILogger<UserService> logger, IMapper mapper, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IUserValidator userValidator)
  {
    _userRepo = userRepo;
    _logger = logger;
    _mapper = mapper;
    _passwordHasher = passwordHasher;
    _jwtProvider = jwtProvider;
    _userValidator = userValidator;
  }

  public async Task<BaseResult<CreateUserResponse>> CreateAsync(CreateUserRequest dto)
  {
    var user = _mapper.Map<User>(dto);
    var hashedPassword = _passwordHasher.Generate(dto.Password);
    user.Password = hashedPassword;

    _logger.LogInformation(user.ToString()!);
    await _userRepo.CreateAsync(user);
    await _userRepo.SaveChangesAsync();
    return BaseResult<CreateUserResponse>
      .Success(_mapper.Map<CreateUserResponse>(user));
  }

  public async Task<BaseResult<LoginUserResponse>> LoginAsync(LoginUserRequest dto)
  {
    var user = await _userRepo.GetByUsernameAsync(dto.Username);
    if (user == null)
    {
      return BaseResult<LoginUserResponse>
        .Error("User not found", (int)HttpStatusCode.NotFound);
    }

    var isCorrect = _passwordHasher.Verify(dto.Password, user.Password);
    if (!isCorrect)
    {
      return BaseResult<LoginUserResponse>
        .Error("Incorrect login or password", (int)HttpStatusCode.BadRequest);
    }

    var token = _jwtProvider.GenerateToken(user);
    LoginUserResponse res = new LoginUserResponse() { AccessToken = token };

    return BaseResult<LoginUserResponse>.Success(res);
  }

  public async Task<BaseResult<UserResponse>> GetByIdAsync(Guid userId)
  {
    var user = await _userRepo.GetByIdAsync(userId);
    if (user == null)
    {
      return BaseResult<UserResponse>
        .Error("User not found", (int)HttpStatusCode.NotFound);
    }

    return BaseResult<UserResponse>
        .Success(_mapper.Map<UserResponse>(user));
  }

  public async Task<BaseResult> UpdateAsync(UpdateUserRequest dto)
  {
    var user = _mapper.Map<User>(dto);
    _logger.LogInformation(user.ToString()!);
    await _userRepo.SaveChangesAsync();
    return BaseResult
      .Success();
  }

  public async Task<BaseResult> DeleteAsync(DeleteUserRequest dto)
  {
    var user = await _userRepo.GetByIdAsync(dto.Id);
    _userRepo.Delete(user);
    _logger.LogInformation($"Deleted user with ID: {user.Id}");
    await _userRepo.SaveChangesAsync();
    return BaseResult
      .Success();
  }
}