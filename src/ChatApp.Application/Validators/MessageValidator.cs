using ChatApp.Core.DTOs.Chats;
using ChatApp.Core.DTOs.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace ChatApp.Application.Validators;

public class MessageValidator
{
	IValidator<CreateMessageRequest> _createRequestValidator;
	IValidator<UpdateMessageRequest> _updateRequestValidator;
	IValidator<DeleteMessageRequest> _deleteRequestValidator;

	public MessageValidator(IValidator<CreateMessageRequest> createRequestValidator, IValidator<UpdateMessageRequest> updateRequestValidator, IValidator<DeleteMessageRequest> deleteRequestValidator)
	{
		_createRequestValidator = createRequestValidator;
		_updateRequestValidator = updateRequestValidator;
		_deleteRequestValidator = deleteRequestValidator;
	}

	public async Task<ValidationResult> ValidateCreateRequestAsync(CreateMessageRequest dto)
	{
		return await _createRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateUpdateRequestAsync(UpdateMessageRequest dto)
	{
		return await _updateRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateDeleteRequestAsync(DeleteMessageRequest dto)
	{
		return await _deleteRequestValidator.ValidateAsync(dto);
	}
}