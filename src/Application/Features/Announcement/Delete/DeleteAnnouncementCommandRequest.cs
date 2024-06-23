using Core.ResultPattern;
using MediatR;

namespace Application.Features.Announcement.Delete;

public class DeleteAnnouncementCommandRequest : IRequest<IResult>
{
    public Guid AnnouncementId { get; set; }
}