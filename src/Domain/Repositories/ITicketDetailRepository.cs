using Core.GenericRepository;
using Domain.Entities;

namespace Domain.Repositories;

public interface ITicketDetailRepository : IGenericRepository<TicketDetail>
{
    public Task<List<TicketDetail>> GetTicketDetails(Guid ticketId, CancellationToken cancellationToken);
}