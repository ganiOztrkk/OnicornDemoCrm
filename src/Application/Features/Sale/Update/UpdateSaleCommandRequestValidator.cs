using FluentValidation;

namespace Application.Features.Sale.Update;

public class UpdateSaleCommandRequestValidator : AbstractValidator<UpdateSaleCommandRequest>
{
    public UpdateSaleCommandRequestValidator()
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