using FluentValidation;

namespace Application.Features.Meeting.Delete;

public class DeleteMeetingCommandRequestValidator : AbstractValidator<DeleteMeetingCommandRequest>
{
    public DeleteMeetingCommandRequestValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Toplantý ID boþ olamaz.")
            .NotNull().WithMessage("Toplantý ID boþ olamaz.");
    }
}