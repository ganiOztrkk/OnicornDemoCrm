using FluentValidation;
using Mvc.Models;
using Mvc.Models.Customer;

namespace Mvc.Validators;

public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.CompanyName))
            .WithMessage("Müşteri adı ve firma adı birlikte boş olamaz. En az birisi dolu olmalıdır.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz.")
            .NotNull().WithMessage("Adres boş olamaz.")
            .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon boş olamaz.")
            .NotNull().WithMessage("Telefon boş olamaz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .NotNull().WithMessage("Email boş olamaz.")
            .MaximumLength(50).WithMessage("Email en fazla 50 karakter olabilir.");

        RuleFor(x => x.ContactPerson)
            .NotEmpty().WithMessage("İletişim kişisi boş olamaz.")
            .NotNull().WithMessage("İletişim kişisi boş olamaz.")
            .MaximumLength(50).WithMessage("İletişim kişisi en fazla 50 karakter olabilir.");
    }
}