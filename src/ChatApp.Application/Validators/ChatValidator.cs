using ChatApp.Core.DTOs.Chats;
using FluentValidation;
using FluentValidation.Results;

namespace ChatApp.Application.Validators;

public class ChatValidator
{
	IValidator<CreateChatRequest> _createRequestValidator;
	IValidator<UpdateChatRequest> _updateRequestValidator;
	IValidator<DeleteChatRequest> _deleteRequestValidator;

	public ChatValidator(IValidator<CreateChatRequest> createRequestValidator, IValidator<UpdateChatRequest> updateRequestValidator, IValidator<DeleteChatRequest> deleteRequestValidator)
	{
		_createRequestValidator = createRequestValidator;
		_updateRequestValidator = updateRequestValidator;
		_deleteRequestValidator = deleteRequestValidator;
	}

	public async Task<ValidationResult> ValidateCreateRequestAsync(CreateChatRequest dto)
	{
		return await _createRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateUpdateRequestAsync(UpdateChatRequest dto)
	{
		return await _updateRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateDeleteRequestAsync(DeleteChatRequest dto)
	{
		return await _deleteRequestValidator.ValidateAsync(dto);
	}
}