using ChatApp.Core.DTOs.Chats;
using FluentValidation;

namespace ChatApp.Application.Validators.Chat;

public class DeleteChatRequestValidator : AbstractValidator<DeleteChatRequest>
{
	public DeleteChatRequestValidator()
	{
		RuleFor(c => c.Id)
			.NotEmpty().WithMessage("Id can't be empty");

		RuleFor(c => c.CreatorId)
			.NotEmpty().WithMessage("Creator id can't be empty");
	}
}