using FluentValidation;

namespace Application.Features.User.GetById;

public class GetByUserIdQueryRequestValidator : AbstractValidator<GetByUserIdQueryRequest>
{
    public GetByUserIdQueryRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Id boş olamaz")
            .NotNull().WithMessage("Id boş olamaz");
    }
}