using ChatApp.Core.DTOs.User;
using FluentValidation;

namespace ChatApp.Application.Validators.User;

public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
{
	public DeleteUserRequestValidator()
	{
		RuleFor(u => u.Id)
			.NotEmpty().WithMessage("Id can't be empty");
	}
}