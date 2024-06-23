using FluentValidation;

namespace Application.Features.Meeting.Create;

public class CreateMeetingCommandRequestValidator : AbstractValidator<CreateMeetingCommandRequest>
{
    public CreateMeetingCommandRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .NotNull().WithMessage("Başlık boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .NotNull().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.")
            .NotNull().WithMessage("Başlangıç tarihi boş olamaz.");

        RuleFor(x => x.Duration)
            .GreaterThan(0).WithMessage("Toplanı süresi sıfırdan büyük olmalıdır.");
    }
}