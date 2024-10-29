using AutoMapper;
using ChatApp.Core.DTOs.Chats;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using Serilog;
using ChatApp.Core.Interfaces.Validators;

namespace ChatApp.Application.Services;

public class ChatService : IChatService
{
  private readonly IChatRepository _chatRepo;
  private readonly IUserRepository _userRepo;
  private readonly ILogger _logger;
  private readonly IMapper _mapper;
  private readonly IChatValidator _chatValidator;

  public ChatService(IChatRepository chatRepo, IUserRepository userRepo, ILogger logger, IMapper mapper, IChatValidator chatValidator)
  {
    _chatRepo = chatRepo;
    _userRepo = userRepo;
    _logger = logger;
    _mapper = mapper;
    _chatValidator = chatValidator;
  }

  public async Task<BaseResult<CreateChatResponse>> CreateAsync(CreateChatRequest dto)
  {
    var chat = _mapper.Map<Chat>(dto);
    if (chat == null)
    {
      throw new Exception("Chat not found");
    }
    _logger.Information(chat.ToString()!);

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

    return BaseResult<CreateChatResponse>
      .Success(_mapper.Map<CreateChatResponse>(chat));
  }

  public async Task<BaseResult<ChatResponse>> GetByIdAsync(Guid chatId)
  {
    var chat = await _chatRepo.GetByIdAsync(chatId);
    if (chat == null)
    {
      throw new Exception("Chat not found");
    }

    return BaseResult<ChatResponse>.Success(_mapper.Map<ChatResponse>(chat));
  }

  public async Task<BaseResult> UpdateAsync(UpdateChatRequest dto)
  {
    var chat = _mapper.Map<Chat>(dto);
    if (chat == null)
    {
      throw new Exception("Chat not found");
    }
    _logger.Information(chat.ToString()!);
    await _chatRepo.SaveChangesAsync();
    return BaseResult.Success();
  }

  public async Task<BaseResult> DeleteAsync(DeleteChatRequest dto)
  {
    var chat = await _chatRepo.GetByIdAsync(dto.Id);
    if (chat == null)
    {
      throw new Exception("Chat not found");
    }
    _logger.Information(chat.ToString()!);
    _chatRepo.Delete(chat);
    await _chatRepo.SaveChangesAsync();
    return BaseResult.Success();
  }
}