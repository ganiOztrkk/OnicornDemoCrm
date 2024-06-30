using FluentValidation;
using Mvc.Models.Task;

namespace Mvc.Validators;

public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .NotNull().WithMessage("Başlık boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .NotNull().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.Deadline)
            .NotEmpty().WithMessage("Deadline tarihi boş olamaz.")
            .NotNull().WithMessage("Deadline tarihi boş olamaz.");

        RuleFor(x => x.UserIds)
            .NotEmpty().WithMessage("UserIds boş olamaz.")
            .Must(userIds => userIds.Count > 0).WithMessage("En az bir kullanıcıya atama olmalı.");
    }
}