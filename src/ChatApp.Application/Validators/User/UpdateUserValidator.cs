using ChatApp.Core.DTOs.User;
using FluentValidation;

namespace ChatApp.Application.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(u => u.Id)
			.NotEmpty().WithMessage("Id can't be empty");

		RuleFor(u => u.Password)
			.MinimumLength(8)
			.When(u => u.Password != null)
			.WithMessage("Password should be at least 8 characters long")
			.MaximumLength(24)
			.When(u => u.Password != null)
			.WithMessage("Password should be at most 24 characters long");
	}
}