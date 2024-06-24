using FluentValidation;

namespace Application.Features.Meeting.GetByUserId
{
    public class GetMeetingByUserIdQueryRequestValidator : AbstractValidator<GetMeetingByUserIdQueryRequest>
    {
        public GetMeetingByUserIdQueryRequestValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID boş olamaz.")
            .NotNull().WithMessage("ID boş olamaz.");
        }
    }
}
