using Core.GenericRepository;

namespace Domain.Entities;

public sealed class MeetingAttendee : IEntity
{
    public Guid MeetingId { get; set; }
    public Meeting? Meeting { get; set; }
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}