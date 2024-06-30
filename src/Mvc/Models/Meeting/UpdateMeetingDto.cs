namespace Mvc.Models.Meeting;

public class UpdateMeetingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
}