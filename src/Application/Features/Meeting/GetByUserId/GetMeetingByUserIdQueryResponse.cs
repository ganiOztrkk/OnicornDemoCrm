namespace Application.Features.Meeting.GetByUserId;

public class GetMeetingByUserIdQueryResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
}