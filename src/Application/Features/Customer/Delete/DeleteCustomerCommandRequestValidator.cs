using FluentValidation;

namespace Application.Features.Customer.Delete;

public class DeleteCustomerCommandRequestValidator : AbstractValidator<DeleteCustomerCommandRequest>
{
    public DeleteCustomerCommandRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id boş olamaz")
            .NotNull().WithMessage("Id boş olamaz");
    }
}