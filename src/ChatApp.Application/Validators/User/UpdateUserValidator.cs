using ChatApp.Core.DTOs.Users;
using FluentValidation;

namespace ChatApp.Application.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(u => u.Id)
			.NotEmpty().WithMessage("Id can't be empty");
	}
}