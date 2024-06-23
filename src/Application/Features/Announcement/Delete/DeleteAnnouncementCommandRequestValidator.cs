using FluentValidation;

namespace Application.Features.Announcement.Delete;

public class DeleteAnnouncementCommandRequestValidator : AbstractValidator<DeleteAnnouncementCommandRequest>
{
    public DeleteAnnouncementCommandRequestValidator()
    {
        RuleFor(x => x.AnnouncementId)
            .NotEmpty().WithMessage("ID boş olamaz")
            .NotNull().WithMessage("ID boş olamaz");
    }
}