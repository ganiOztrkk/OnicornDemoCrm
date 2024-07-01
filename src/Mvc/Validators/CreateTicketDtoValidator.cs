using FluentValidation;
using Mvc.Models.Ticket;

namespace Mvc.Validators;

public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
{
    public CreateTicketDtoValidator()
    {
        RuleFor(x => x.Subject)
            .NotNull().WithMessage("Başlık boş olamaz.")
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(40).WithMessage("Başlık en fazla 40 karakter olabilir.");
        RuleFor(x => x.Summary)
            .NotNull().WithMessage("İçerik boş olamaz.")
            .NotEmpty().WithMessage("İçerik boş olamaz.")
            .MaximumLength(500).WithMessage("İçerik en fazla 500 karakter olabilir.");
    }
}