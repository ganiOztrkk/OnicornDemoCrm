using FluentValidation;

namespace Application.Features.Customer.GetById;

public class GetByCustomerIdQueryRequestValidator : AbstractValidator<GetByCustomerIdQueryRequest>
{
    public GetByCustomerIdQueryRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID Boş olamaz.")
            .NotNull().WithMessage("ID Boş olamaz.");
    }
}