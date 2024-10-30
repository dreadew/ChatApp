using ChatApp.Core.DTOs.Message;
using ChatApp.Core.Interfaces.Validators;
using ChatApp.Core.Models;
using FluentValidation;

namespace ChatApp.Application.Validators;

public class MessageValidator : IMessageValidator
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

	public async Task<ValidationResultModel> ValidateCreateRequestAsync(CreateMessageRequest dto)
	{
		var validationResult = await _createRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}

	public async Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateMessageRequest dto)
	{
		var validationResult = await _updateRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}

	public async Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteMessageRequest dto)
	{
		var validationResult = await _deleteRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}
}