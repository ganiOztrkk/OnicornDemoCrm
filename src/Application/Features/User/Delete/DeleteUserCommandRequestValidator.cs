using FluentValidation;

namespace Application.Features.User.Delete;

public class DeleteUserCommandRequestValidator : AbstractValidator<DeleteUserCommandRequest>
{
    public DeleteUserCommandRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Id boş olamaz")
            .NotNull().WithMessage("Id boş olamaz");
    }
}