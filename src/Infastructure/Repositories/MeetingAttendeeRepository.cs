using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class MeetingAttendeeRepository(ApplicationDbContext context) : GenericRepository<MeetingAttendee, ApplicationDbContext>(context), IMeetingAttendeeRepository
{
    public async Task<List<MeetingAttendee>> GetByMeetingIdAsync(Guid meetingId)
    {
        return await context.MeetingAttendees
            .Where(ma => ma.MeetingId == meetingId)
            .ToListAsync();
    }
}