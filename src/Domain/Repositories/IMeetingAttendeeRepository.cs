using Core.GenericRepository;
using Domain.Entities;

namespace Domain.Repositories;

public interface IMeetingAttendeeRepository : IGenericRepository<MeetingAttendee>
{
    Task<List<MeetingAttendee>> GetByMeetingIdAsync(Guid meetingId);
}