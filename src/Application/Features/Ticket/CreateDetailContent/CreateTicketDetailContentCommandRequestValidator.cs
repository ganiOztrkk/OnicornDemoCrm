using FluentValidation;

namespace Application.Features.Ticket.CreateDetailContent;

public class CreateTicketDetailContentCommandRequestValidator : AbstractValidator<CreateTicketDetailContentCommandRequest>
{
    public CreateTicketDetailContentCommandRequestValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty().WithMessage("Mesaj ID boş olamaz")
            .NotNull().WithMessage("Mesaj ID boş olamaz.");
        RuleFor(x => x.AppUserId)
            .NotEmpty().WithMessage("Mesaj ID boş olamaz")
            .NotNull().WithMessage("Mesaj ID boş olamaz.");
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik boş olamaz")
            .NotNull().WithMessage("İçerik boş olamaz.")
            .MaximumLength(500).WithMessage("Mesaj en fazla 500 karakter olabilir.");
    }
}