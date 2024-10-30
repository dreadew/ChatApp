using AutoMapper;
using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using ChatApp.Core.Interfaces.Validators;
using Microsoft.Extensions.Logging;
using System.Net;
using ChatApp.Core.Exceptions.Chat;

namespace ChatApp.Application.Services;

public class ChatService : IChatService
{
  private readonly IChatRepository _chatRepo;
  private readonly IUserRepository _userRepo;
  private readonly ILogger<ChatService> _logger;
  private readonly IMapper _mapper;
  private readonly IChatValidator _chatValidator;

  public ChatService(IChatRepository chatRepo, IUserRepository userRepo, ILogger<ChatService> logger, IMapper mapper, IChatValidator chatValidator)
  {
    _chatRepo = chatRepo;
    _userRepo = userRepo;
    _logger = logger;
    _mapper = mapper;
    _chatValidator = chatValidator;
  }

  public async Task<BaseResult<CreateChatResponse>> CreateAsync(CreateChatRequest dto)
  {
    var validation = await _chatValidator.ValidateCreateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to create chat '{dto.Name}'\nErrors: {errors}");
      return BaseResult<CreateChatResponse>
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    try
    {
      var chat = _mapper.Map<Chat>(dto);

      await _chatRepo.CreateAsync(chat);

      if (dto.UsersIds != null)
      {
        var users = await _userRepo.GetByIdsAsync(dto.UsersIds);
        await _chatRepo.AppendUsersAsync(chat, users);
      }

      await _chatRepo.SaveChangesAsync();

      _logger.LogInformation($"Successfully created chat with ID '{chat.Id}'");

      return BaseResult<CreateChatResponse>
        .Success(_mapper.Map<CreateChatResponse>(chat));
    }
    catch (ChatAlreadyExistedException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<CreateChatResponse>
        .Error(ex.Message, (int)HttpStatusCode.BadRequest);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<CreateChatResponse>
        .Error("Unknown exception", (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult<ChatResponse>> GetByIdAsync(Guid chatId)
  {
    try
    {
      var chat = await _chatRepo.GetByIdAsync(chatId);
      return BaseResult<ChatResponse>.Success(_mapper.Map<ChatResponse>(chat));
    }
    catch (ChatNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult<ChatResponse>
        .Error(ex.Message, (int)HttpStatusCode.NotFound);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult<ChatResponse>
        .Error("Unknown exception", (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult> UpdateAsync(UpdateChatRequest dto)
  {
    var validation = await _chatValidator.ValidateUpdateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to update chat '{dto.Id}'\nErrors: {errors}");
      return BaseResult
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }
    
    try 
    {
      var chat = await _chatRepo.GetByIdAsync(dto.Id);

      _mapper.Map(dto, chat);

      if (dto.UsersIds != null)
      {
        var users = await _userRepo.GetByIdsAsync(dto.UsersIds);
        await _chatRepo.RemoveUsersAsync(chat, users);
      }

      _chatRepo.Update(chat);

      await _chatRepo.SaveChangesAsync();

      _logger.LogInformation($"Chat with ID '${chat.Id} successfully updated");

      return BaseResult.Success();
    }
    catch (ChatNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.NotFound);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult
        .Error("Unknown exception", (int)HttpStatusCode.InternalServerError);
    }
  }

  public async Task<BaseResult> DeleteAsync(DeleteChatRequest dto)
  {
    try 
    {
      var chat = await _chatRepo.GetByIdAsync(dto.Id);

      _chatRepo.Delete(chat);

      await _chatRepo.SaveChangesAsync();

      _logger.LogInformation($"Deleted chat with ID '{dto.Id}'");

      return BaseResult.Success();
    }
    catch (ChatNotFoundException ex)
    {
      _logger.LogError(ex.Message);
      return BaseResult
        .Error(ex.Message, (int)HttpStatusCode.NotFound);
    }
    catch (Exception ex)
    {
      _logger.LogError($"Unknow exception: {ex.Message}");
      return BaseResult
        .Error("Unknown exception", (int)HttpStatusCode.InternalServerError);
    }
  }
}