namespace Mvc.Models.Announcement;

public class GetAllAnnouncementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}