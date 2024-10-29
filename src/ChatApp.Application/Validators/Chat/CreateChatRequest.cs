using ChatApp.Core.DTOs.Chats;
using FluentValidation;

namespace ChatApp.Application.Validators.Chat;

public class CreateChatRequestValidator : AbstractValidator<CreateChatRequest>
{
	public CreateChatRequestValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("Name can't be empty")
			.MinimumLength(3).WithMessage("Name should be at least 6 characters long")
			.MaximumLength(16).WithMessage("Name should be at most 16 characters long");

		RuleFor(c => c.CreatorId)
			.NotEmpty().WithMessage("Creator id can't be empty");

		RuleFor(c => c.UsersIds)
			.NotEmpty().WithMessage("Users id can't be empty");
	}
}