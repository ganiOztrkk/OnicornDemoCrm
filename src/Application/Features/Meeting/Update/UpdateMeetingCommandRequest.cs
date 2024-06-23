using Core.ResultPattern;
using MediatR;

namespace Application.Features.Meeting.Update;

public class UpdateMeetingCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
}