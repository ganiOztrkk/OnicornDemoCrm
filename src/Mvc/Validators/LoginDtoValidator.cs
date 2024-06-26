using FluentValidation;
using Mvc.Models;

namespace Mvc.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotNull().WithMessage("Kullanıcı boş olamaz")
            .NotEmpty().WithMessage("Kullanıcı boş olamaz")
            .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter içermelidir");
        RuleFor(p => p.Password)
            .MinimumLength(1).WithMessage("Şifre en az 1 karakter olmalıdır")
            .NotNull().WithMessage("Şifre boş olamaz")
            .NotEmpty().WithMessage("Şifre boş olamaz");
    }
}