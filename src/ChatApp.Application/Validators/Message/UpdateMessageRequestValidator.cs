using ChatApp.Core.DTOs.Messages;
using FluentValidation;

namespace ChatApp.Application.Validators.Message;

public class UpdateMessageRequestValidator : AbstractValidator<UpdateMessageRequest>
{
	public UpdateMessageRequestValidator()
	{
		RuleFor(c => c.Id)
			.NotEmpty().WithMessage("Id can't be empty");

		RuleFor(c => c.Content)
			.NotEmpty().WithMessage("Content can't be empty");

		RuleFor(c => c.SenderId)
			.NotEmpty().WithMessage("Sender id can't be empty");
	}
}