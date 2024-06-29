using FluentValidation;
using Mvc.Models;
using Mvc.Models.User;

namespace Mvc.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim boş olamaz.")
            .NotNull().WithMessage("İsim boş olamaz.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Soyisim boş olamaz.")
            .NotNull().WithMessage("Soyisim boş olamaz.");

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