using FluentValidation;

namespace Application.Features.Task.GetByUserId
{
    public class GetTaskByUserIdQueryRequestValidator : AbstractValidator<GetTaskByUserIdQueryRequest>
    {
        public GetTaskByUserIdQueryRequestValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID boş olamaz.")
            .NotNull().WithMessage("ID boş olamaz.");
        }
    }
}
