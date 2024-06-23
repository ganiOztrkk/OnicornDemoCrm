using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class AnnouncementRepository(ApplicationDbContext context) : GenericRepository<Announcement, ApplicationDbContext>(context), IAnnouncementRepository
{
    public async Task<List<Announcement>> GetLatestAnnouncementsAsync(CancellationToken cancellationToken)
    {
        var announcements = await context.Announcements
            .Take(5)
            .ToListAsync(cancellationToken);
        return announcements;
    }
}