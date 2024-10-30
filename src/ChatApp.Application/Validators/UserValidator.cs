using ChatApp.Core.DTOs.User;
using ChatApp.Core.Interfaces.Validators;
using ChatApp.Core.Models;
using FluentValidation;

namespace ChatApp.Application.Validators;

public class UserValidator : IUserValidator
{
	IValidator<CreateUserRequest> _createRequestValidator;
	IValidator<LoginUserRequest> _loginRequestValidator;
	IValidator<UpdateUserRequest> _updateRequestValidator;
	IValidator<DeleteUserRequest> _deleteRequestValidator;

	public UserValidator(IValidator<CreateUserRequest> createRequestValidator, IValidator<LoginUserRequest> loginRequestValidator, IValidator<UpdateUserRequest> updateRequestValidator, IValidator<DeleteUserRequest> deleteRequestValidator)
	{
		_createRequestValidator = createRequestValidator;
		_loginRequestValidator = loginRequestValidator;
		_updateRequestValidator = updateRequestValidator;
		_deleteRequestValidator = deleteRequestValidator;
	}

	public async Task<ValidationResultModel> ValidateCreateRequestAsync(CreateUserRequest dto)
	{
		var validationResult = await _createRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
  }

	public async Task<ValidationResultModel> ValidateLoginRequestAsync(LoginUserRequest dto)
	{
		var validationResult = await _loginRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
  }

	public async Task<ValidationResultModel> ValidateUpdateRequestAsync(UpdateUserRequest dto)
	{
		var validationResult = await _updateRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}

	public async Task<ValidationResultModel> ValidateDeleteRequestAsync(DeleteUserRequest dto)
	{
		var validationResult = await _deleteRequestValidator.ValidateAsync(dto);
    return new ValidationResultModel
    {
      IsValid = validationResult.IsValid,
      Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
    };
	}
}