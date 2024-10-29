using AutoMapper;
using ChatApp.Core.Entities;
using ChatApp.Core.Interfaces.Repositories;
using ChatApp.Core.Results;
using Serilog;
using ChatApp.Core.DTOs.Messages;
using ChatApp.Core.Interfaces.Services;

namespace ChatApp.Application.Services;

public class MessageService : IMessageService
{
  private readonly IMessageRepository _messageRepo;
  private readonly ILogger _logger;
  private readonly IMapper _mapper;

  public MessageService(IMessageRepository messageRepo, ILogger logger, IMapper mapper)
  {
    _messageRepo = messageRepo;
    _logger = logger;
    _mapper = mapper;
  }

  public async Task<BaseResult<CreateMessageResponse>> CreateAsync(CreateMessageRequest dto)
  {
    var message = _mapper.Map<Message>(dto);
    if (message == null)
    {
      throw new Exception("Message not found");
    }
    _logger.Information(message.ToString()!);

    await _messageRepo.CreateAsync(message);
    await _messageRepo.SaveChangesAsync();

    return BaseResult<CreateMessageResponse>
      .Success(_mapper.Map<CreateMessageResponse>(message));
  }

  public async Task<BaseResult<MessageResponse>> GetByIdAsync(Guid messageId)
  {
    var message = await _messageRepo.GetByIdAsync(messageId);
    if (message == null)
    {
      throw new Exception("Message not found");
    }

    return BaseResult<MessageResponse>.Success(_mapper.Map<MessageResponse>(message));
  }

  public async Task<BaseResult<List<MessageResponse>>> ListByChatAsync(Guid chatId)
  {
    var messages = await _messageRepo.ListByChatAsync(chatId);
    if (messages == null)
    {
      throw new Exception("Messages not found");
    }

    return BaseResult<List<MessageResponse>>.Success(_mapper.Map<List<MessageResponse>>(messages));
  }

  public async Task<BaseResult> UpdateAsync(UpdateMessageRequest dto)
  {
    var message = _mapper.Map<Message>(dto);
    if (message == null)
    {
      throw new Exception("Message not found");
    }
    _logger.Information(message.ToString()!);
    _messageRepo.Update(message);
    await _messageRepo.SaveChangesAsync();
    return BaseResult.Success();
  }

  public async Task<BaseResult> DeleteAsync(DeleteMessageRequest dto)
  {
    var message = await _messageRepo.GetByIdAsync(dto.Id);
    if (message == null)
    {
      throw new Exception("Message not found");
    }
    _logger.Information(message.ToString()!);
    _messageRepo.Delete(message);
    await _messageRepo.SaveChangesAsync();
    return BaseResult.Success();
  }
}