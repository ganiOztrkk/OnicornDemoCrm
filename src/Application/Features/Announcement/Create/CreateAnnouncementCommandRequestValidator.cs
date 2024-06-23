using FluentValidation;

namespace Application.Features.Announcement.Create;

public class CreateAnnouncementCommandRequestValidator : AbstractValidator<CreateAnnouncementCommandRequest>
{
    public CreateAnnouncementCommandRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz")
            .NotNull().WithMessage("Başlık boş olamaz");
        
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik boş olamaz")
            .NotNull().WithMessage("İçerik boş olamaz");
    }
}