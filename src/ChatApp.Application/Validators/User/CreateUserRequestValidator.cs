using ChatApp.Core.DTOs.Users;
using FluentValidation;

namespace ChatApp.Application.Validators.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
	public CreateUserRequestValidator()
	{
		RuleFor(u => u.Username)
			.NotEmpty().WithMessage("Username can't be empty")
			.MinimumLength(6).WithMessage("Username should be at least 6 characters long")
			.MaximumLength(16).WithMessage("Username should be at most 16 characters long");

		RuleFor(u => u.Email)
			.NotEmpty().WithMessage("Email can't be empty")
			.EmailAddress().WithMessage("Incorrect email");
	}
}