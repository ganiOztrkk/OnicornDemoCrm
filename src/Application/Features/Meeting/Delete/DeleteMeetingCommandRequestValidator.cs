using FluentValidation;

namespace Application.Features.Meeting.Delete;

public class DeleteMeetingCommandRequestValidator : AbstractValidator<DeleteMeetingCommandRequest>
{
    public DeleteMeetingCommandRequestValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Toplant� ID bo� olamaz.")
            .NotNull().WithMessage("Toplant� ID bo� olamaz.");
    }
}