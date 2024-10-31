using ChatApp.Core.DTOs.Chat;
using FluentValidation;

namespace ChatApp.Application.Validators.Chat;

public class FindOrCreatePrivateChatRequestValidator : AbstractValidator<FindOrCreatePrivateChatRequest>
{
	public FindOrCreatePrivateChatRequestValidator()
	{
		RuleFor(c => c.UsersIds)
			.NotEmpty().WithMessage("Users id can't be empty");
	}
}