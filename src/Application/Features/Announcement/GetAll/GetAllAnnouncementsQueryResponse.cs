namespace Application.Features.Announcement.GetAll;

public class GetAllAnnouncementsQueryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}