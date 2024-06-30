namespace Mvc.Models.Meeting;

public class GetMeetingByUserIdDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
}