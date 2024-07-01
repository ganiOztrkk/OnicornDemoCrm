using FluentValidation;

namespace Application.Features.Ticket.CreateDetailContent;

public class CreateTicketDetailContentCommandRequestValidator : AbstractValidator<CreateTicketDetailContentCommandRequest>
{
    public CreateTicketDetailContentCommandRequestValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty().WithMessage("Ticket ID boş olamaz")
            .NotNull().WithMessage("Ticket ID boş olamaz.");
        RuleFor(x => x.AppUserId)
            .NotEmpty().WithMessage("User ID boş olamaz")
            .NotNull().WithMessage("User ID boş olamaz.");
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik boş olamaz")
            .NotNull().WithMessage("İçerik boş olamaz.")
            .MaximumLength(500).WithMessage("Mesaj en fazla 500 karakter olabilir.");
    }
}