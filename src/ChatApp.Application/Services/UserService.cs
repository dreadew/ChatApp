using System.Net;
using AutoMapper;
using ChatApp.Core.DTOs.User;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Auth;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using ChatApp.Core.Interfaces.Validators;
using Microsoft.Extensions.Logging;
using ChatApp.Core.Exceptions.User;

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
    var validation = await _userValidator.ValidateCreateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to create account '{dto.Email}'\nErrors: {errors}");
      return BaseResult<CreateUserResponse>
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }
    
    var user = _mapper.Map<User>(dto);
    var hashedPassword = _passwordHasher.Generate(dto.Password);
    user.Password = hashedPassword;

    try
    {
      await _userRepo.CreateAsync(user);
      
      await _userRepo.SaveChangesAsync();

      _logger.LogInformation($"Successfully created user with ID '{user.Id}'");

      return BaseResult<CreateUserResponse>
        .Success(_mapper.Map<CreateUserResponse>(user));
    }
    catch (UserAlreadyExistedException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<CreateUserResponse>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<CreateUserResponse>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
  }

  public async Task<BaseResult<LoginUserResponse>> LoginAsync(LoginUserRequest dto)
  {
    var validation = await _userValidator.ValidateLoginRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to Log in '{dto.Username}'\nErrors: {errors}");
      return BaseResult<LoginUserResponse>
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    try
    {
      var user = await _userRepo.GetByUsernameAsync(dto.Username);

      var isCorrect = _passwordHasher.Verify(dto.Password, user.Password);
      if (!isCorrect)
      {
        _logger.LogError($"User '{user.Id}' failed to Log in");
        return BaseResult<LoginUserResponse>
          .Error("Incorrect login or password", (int)HttpStatusCode.BadRequest);
      }

      var token = _jwtProvider.GenerateToken(user);
      LoginUserResponse res = new LoginUserResponse(token);

      _logger.LogInformation($"User '{user.Id}' successfully Logged in");

      return BaseResult<LoginUserResponse>
        .Success(res);
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<LoginUserResponse>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<LoginUserResponse>
        .Error(ex.Message, (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult<UserResponse>> GetByIdAsync(Guid userId)
  {
    try
    {
      var user = await _userRepo.GetByIdAsync(userId);
      return BaseResult<UserResponse>
        .Success(_mapper.Map<UserResponse>(user));
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<UserResponse>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<UserResponse>
        .Error(ex.Message, (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult<List<UserResponse>>> ListAsync(ListUserRequest dto)
  {
    try
    {
      var users = await _userRepo.ListAsync(dto.Take, dto.Skip);
      return BaseResult<List<UserResponse>>
        .Success(_mapper.Map<List<UserResponse>>(users));
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<List<UserResponse>>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<List<UserResponse>>
        .Error(ex.Message, (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult> UpdateAsync(UpdateUserRequest dto)
  {
    var validation = await _userValidator.ValidateUpdateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to update account '{dto.Id}'\nErrors: {errors}");
      return BaseResult
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    try
    {
      var user = await _userRepo.GetByIdAsync(dto.Id);

      _mapper.Map(dto, user);
      
      if (dto.Password != null)
      {
        var hashedPassword = _passwordHasher.Generate(dto.Password);
        user.Password = hashedPassword;
      }

      _userRepo.Update(user);

      await _userRepo.SaveChangesAsync();

      _logger.LogInformation($"User with ID '${user.Id} successfully updated");

      return BaseResult
        .Success();
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult> DeleteAsync(DeleteUserRequest dto)
  {
    var validation = await _userValidator.ValidateDeleteRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to delete account '{dto.Id}'\nErrors: {errors}");
      return BaseResult
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    try
    {
      var user = await _userRepo.GetByIdAsync(dto.Id);

      _userRepo.Delete(user);

      await _userRepo.SaveChangesAsync();

      _logger.LogInformation($"Deleted user with ID '{user.Id}'");

      return BaseResult
        .Success();
    }
    catch (UserNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.InternalServerError);
    }
  }
}