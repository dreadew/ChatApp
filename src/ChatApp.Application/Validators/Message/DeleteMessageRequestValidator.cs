using ChatApp.Core.DTOs.Message;
using FluentValidation;

namespace ChatApp.Application.Validators.Message;

public class DeleteMessageRequestValidator : AbstractValidator<DeleteMessageRequest>
{
	public DeleteMessageRequestValidator()
	{
		RuleFor(c => c.Id)
			.NotEmpty().WithMessage("Id can't be empty");

		RuleFor(c => c.SenderId)
			.NotEmpty().WithMessage("Sender id can't be empty");
	}
}