using FluentValidation;
using Mvc.Models.Ticket;

namespace Mvc.Validators;

public class CreateTicketDetailDtoValidator : AbstractValidator<CreateTicketDetailDto>
{
    public CreateTicketDetailDtoValidator()
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