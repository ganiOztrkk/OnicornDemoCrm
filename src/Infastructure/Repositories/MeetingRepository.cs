using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class MeetingRepository(ApplicationDbContext context)
    : GenericRepository<Meeting, ApplicationDbContext>(context), IMeetingRepository
{
    public async Task<List<Meeting>> GetMeetingsByUserIdAsync(Guid userId)
    {
        return (await context.MeetingAttendees
            .Where(ma => ma.UserId == userId)
            .Select(ma => ma.Meeting)
            .ToListAsync())!;
    }
}