using Core.GenericRepository;
using Domain.Entities;

namespace Domain.Repositories;

public interface IAnnouncementRepository : IGenericRepository<Announcement>
{
    public Task<List<Announcement>> GetLatestAnnouncementsAsync(CancellationToken cancellationToken);
}