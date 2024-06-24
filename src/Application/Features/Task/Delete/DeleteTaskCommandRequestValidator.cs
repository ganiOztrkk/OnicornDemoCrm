using FluentValidation;

namespace Application.Features.Task.Delete
{
    public class DeleteTaskCommandRequestValidator : AbstractValidator<DeleteTaskCommandRequest>
    {
        public DeleteTaskCommandRequestValidator()
        {
            RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Görev ID boş olamaz.")
            .NotNull().WithMessage("Görev ID boş olamaz.");
        }
    }
}
