using FluentValidation;
using Mvc.Models.Announcement;

namespace Mvc.Validators;

public class CreateAnnouncementDtoValidator : AbstractValidator<CreateAnnouncementDto>
{
    public CreateAnnouncementDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz")
            .NotNull().WithMessage("Başlık boş olamaz");
        
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik boş olamaz")
            .NotNull().WithMessage("İçerik boş olamaz");
    }
}