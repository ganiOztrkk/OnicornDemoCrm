using FluentValidation;

namespace Application.Features.Sale.Delete;

public class DeleteSaleCommandRequestValidator : AbstractValidator<DeleteSaleCommandRequest>
{
    public DeleteSaleCommandRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id boş olamaz")
            .NotNull().WithMessage("Id boş olamaz");
    }
}