using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Meeting : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public bool IsDeleted { get; set; } = false;

    public ICollection<MeetingAttendee> Attendees { get; set; } = new List<MeetingAttendee>();
}