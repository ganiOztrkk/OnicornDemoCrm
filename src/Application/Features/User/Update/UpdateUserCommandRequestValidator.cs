using FluentValidation;

namespace Application.Features.User.Update;

public class UpdateUserCommandRequestValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
            .NotNull().WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Rol adı boş olamaz.")
            .NotNull().WithMessage("Rol adı boş olamaz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .NotNull().WithMessage("Şifre boş olamaz.");
    }
}