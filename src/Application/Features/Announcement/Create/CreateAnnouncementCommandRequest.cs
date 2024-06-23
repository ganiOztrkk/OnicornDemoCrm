using Core.ResultPattern;
using MediatR;

namespace Application.Features.Announcement.Create;

public class CreateAnnouncementCommandRequest : IRequest<IResult>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}