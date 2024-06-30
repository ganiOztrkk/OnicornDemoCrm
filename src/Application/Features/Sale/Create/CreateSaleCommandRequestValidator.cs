using FluentValidation;

namespace Application.Features.Sale.Create;

public class CreateSaleCommandRequestValidator : AbstractValidator<CreateSaleCommandRequest>
{
    public CreateSaleCommandRequestValidator()
    {
        RuleFor(x =>x.Amount)
            .NotEmpty().WithMessage("Tutar boş olamaz.")
            .NotNull().WithMessage("Tutar boş olamaz.");
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müşteri ID boş olamaz.")
            .NotNull().WithMessage("Müşteri ID boş olamaz.");
        RuleFor(x => x.SalesPersonId)
            .NotEmpty().WithMessage("Satış personeli ID boş olamaz.")
            .NotNull().WithMessage("Satış personeli ID boş olamaz.");
        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Satış açıklama en fazla 100 karakter olmalı.");
    }
}