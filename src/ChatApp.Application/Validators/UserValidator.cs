using ChatApp.Core.DTOs.Users;
using FluentValidation;
using FluentValidation.Results;

namespace ChatApp.Application.Validators;

public class UserValidator
{
	IValidator<CreateUserRequest> _createRequestValidator;
	IValidator<UpdateUserRequest> _updateRequestValidator;
	IValidator<DeleteUserRequest> _deleteRequestValidator;

	public UserValidator(IValidator<CreateUserRequest> createRequestValidator, IValidator<UpdateUserRequest> updateRequestValidator, IValidator<DeleteUserRequest> deleteRequestValidator)
	{
		_createRequestValidator = createRequestValidator;
		_updateRequestValidator = updateRequestValidator;
		_deleteRequestValidator = deleteRequestValidator;
	}

	public async Task<ValidationResult> ValidateCreateRequestAsync(CreateUserRequest dto)
	{
		return await _createRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateUpdateRequestAsync(UpdateUserRequest dto)
	{
		return await _updateRequestValidator.ValidateAsync(dto);
	}

	public async Task<ValidationResult> ValidateDeleteRequestAsync(DeleteUserRequest dto)
	{
		return await _deleteRequestValidator.ValidateAsync(dto);
	}
}