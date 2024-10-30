using ChatApp.Core.DTOs.Chat;
using ChatApp.Core.Interfaces.Validators;
using ChatApp.Core.Models;
using FluentValidation;

namespace ChatApp.Application.Validators;

public class ChatValidator : IChatValidator
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

	public async Task<ValidationResultModel> ValidateCreateRequestAsync(CreateChatRequest dto)
	{
		var validationResult = await _createRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}

	public async Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateChatRequest dto)
	{
		var validationResult = await _updateRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}

	public async Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteChatRequest dto)
	{
		var validationResult = await _deleteRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}
}