using FluentValidation;

namespace Application.Features.Sale.GetById;

public class GetBySaleIdQueryRequestValidator : AbstractValidator<GetBySaleIdQueryRequest>
{
    public GetBySaleIdQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID Boş olamaz.")
            .NotNull().WithMessage("ID Boş olamaz.");
    }
}