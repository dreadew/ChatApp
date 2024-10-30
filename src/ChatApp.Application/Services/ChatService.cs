using AutoMapper;
using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using ChatApp.Core.Interfaces.Validators;
using Microsoft.Extensions.Logging;
using System.Net;

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

    var chat = _mapper.Map<Chat>(dto);

    await _chatRepo.CreateAsync(chat);

    if (dto.UsersIds != null)
    {
      var users = await _userRepo.GetByIdsAsync(dto.UsersIds);
      if (users != null)
      {
        foreach (var user in users)
        {
          chat.Users?.Add(user);
        }
      }
    }

    await _chatRepo.SaveChangesAsync();

    _logger.LogInformation($"Successfully created chat with ID '{chat.Id}'");

    return BaseResult<CreateChatResponse>
      .Success(_mapper.Map<CreateChatResponse>(chat));
  }

  public async Task<BaseResult<ChatResponse>> GetByIdAsync(Guid chatId)
  {
    var chat = await _chatRepo.GetByIdAsync(chatId);
    if (chat == null)
    {
      _logger.LogError($"Chat with ID '{chatId}' not found");
      return BaseResult<ChatResponse>
        .Error("Chat not found", (int)HttpStatusCode.NotFound);
    }

    return BaseResult<ChatResponse>.Success(_mapper.Map<ChatResponse>(chat));
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
    
    var chat = await _chatRepo.GetByIdAsync(dto.Id);
    if (chat == null)
    {
      _logger.LogError($"Chat with ID '{dto.Id}' not found");
      return BaseResult<ChatResponse>
        .Error("Chat not found", (int)HttpStatusCode.NotFound);
    }

    _mapper.Map(dto, chat);

    _chatRepo.Update(chat);

    await _chatRepo.SaveChangesAsync();

    _logger.LogInformation($"Chat with ID '${chat.Id} successfully updated");

    return BaseResult.Success();
  }

  public async Task<BaseResult> DeleteAsync(DeleteChatRequest dto)
  {
    var chat = await _chatRepo.GetByIdAsync(dto.Id);
    if (chat == null)
    {
      _logger.LogError($"Chat with ID '{dto.Id}' not found");
      return BaseResult<ChatResponse>
        .Error("Chat not found", (int)HttpStatusCode.NotFound);
    }

    _chatRepo.Delete(chat);

    await _chatRepo.SaveChangesAsync();

    _logger.LogInformation($"Deleted chat with ID '{dto.Id}'");

    return BaseResult.Success();
  }
}