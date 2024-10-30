using AutoMapper;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Interfaces.Services;
using ChatApp.Core.Interfaces.Validators;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ChatApp.Application.Services;

public class MessageService : IMessageService
{
  private readonly IMessageRepository _messageRepo;
  private readonly ILogger<MessageService> _logger;
  private readonly IMapper _mapper;
  private readonly IMessageValidator _messageValidator;

  public MessageService(IMessageRepository messageRepo, ILogger<MessageService> logger, IMapper mapper, IMessageValidator messageValidator)
  {
    _messageRepo = messageRepo;
    _logger = logger;
    _mapper = mapper;
    _messageValidator = messageValidator;
  }

  public async Task<BaseResult<CreateMessageResponse>> CreateAsync(CreateMessageRequest dto)
  {
    var validation = await _messageValidator.ValidateCreateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to create message '{dto.SenderId}'\nErrors: {errors}");
      return BaseResult<CreateMessageResponse>
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    var message = _mapper.Map<Message>(dto);

    await _messageRepo.CreateAsync(message);

    await _messageRepo.SaveChangesAsync();

    _logger.LogInformation($"Successfully created message with ID '{message.Id}'");

    return BaseResult<CreateMessageResponse>
      .Success(_mapper.Map<CreateMessageResponse>(message));
  }

  public async Task<BaseResult<MessageResponse>> GetByIdAsync(Guid messageId)
  {
    var message = await _messageRepo.GetByIdAsync(messageId);
    if (message == null)
    {
      _logger.LogError($"Message with ID '{messageId}' not found");
      return BaseResult<MessageResponse>
        .Error("Message not found", (int)HttpStatusCode.NotFound);
    }

    return BaseResult<MessageResponse>.Success(_mapper.Map<MessageResponse>(message));
  }

  public async Task<BaseResult<List<MessageResponse>>> ListByChatAsync(Guid chatId)
  {
    var messages = await _messageRepo.ListByChatAsync(chatId);
    if (messages == null)
    {
      _logger.LogError($"Messages with Chat ID '{chatId}' not found");
      return BaseResult<List<MessageResponse>>
        .Error("Messages in this chat not found", (int)HttpStatusCode.NotFound);
    }

    return BaseResult<List<MessageResponse>>.Success(_mapper.Map<List<MessageResponse>>(messages));
  }

  public async Task<BaseResult> UpdateAsync(UpdateMessageRequest dto)
  {
    var validation = await _messageValidator.ValidateUpdateRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to update message '{dto.SenderId}'\nErrors: {errors}");
      return BaseResult
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    var message = await _messageRepo.GetByIdAsync(dto.Id);
    if (message == null)
    {
      _logger.LogError($"Message with ID '{dto.Id}' not found");
      return BaseResult
        .Error("Message not found", (int)HttpStatusCode.NotFound);
    }

    if (message.SenderId != dto.SenderId)
    {
      _logger.LogError($"Error: User with ID '${dto.SenderId}' tried to update Message with ID '{dto.Id}'");
      return BaseResult
        .Error("You can't edit this message", (int)HttpStatusCode.Forbidden);
    }

    _mapper.Map(dto, message);

    _messageRepo.Update(message);

    await _messageRepo.SaveChangesAsync();

    _logger.LogInformation($"Message with ID '${message.Id} successfully updated");

    return BaseResult.Success();
  }

  public async Task<BaseResult> DeleteAsync(DeleteMessageRequest dto)
  {
    var validation = await _messageValidator.ValidateDeleteRequestAsync(dto);
    string errors = string.Join(", ", validation.Errors);
    if (!validation.IsValid)
    {
      _logger.LogError($"Failed to delete message '{dto.SenderId}'\nErrors: {errors}");
      return BaseResult
        .Error($"Validation failed\nErrors:{errors}", (int)HttpStatusCode.BadRequest);
    }

    var message = await _messageRepo.GetByIdAsync(dto.Id);
    if (message == null)
    {
      _logger.LogError($"Message with ID '{dto.Id}' not found");
      return BaseResult
        .Error("Message not found", (int)HttpStatusCode.NotFound);
    }

    _messageRepo.Delete(message);

    await _messageRepo.SaveChangesAsync();

    _logger.LogInformation($"Deleted message with ID '{dto.Id}'");

    return BaseResult.Success();
  }
}