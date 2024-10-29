using ChatApp.Core.DTOs.Messages;
using FluentValidation;

namespace ChatApp.Application.Validators.Message;

public class CreateMessageRequestValidator : AbstractValidator<CreateMessageRequest>
{
	public CreateMessageRequestValidator()
	{
		RuleFor(c => c.ChatId)
			.NotEmpty().WithMessage("Chat Id can't be empty");

		RuleFor(c => c.Content)
			.NotEmpty().WithMessage("Content can't be empty");

		RuleFor(c => c.SenderId)
			.NotEmpty().WithMessage("Sender id can't be empty");
	}
}