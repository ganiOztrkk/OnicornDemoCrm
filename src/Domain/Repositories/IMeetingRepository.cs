using Core.GenericRepository;
using Domain.Entities;

namespace Domain.Repositories;

public interface IMeetingRepository : IGenericRepository<Meeting>
{
    Task<List<Meeting>> GetMeetingsByUserIdAsync(Guid userId);
}