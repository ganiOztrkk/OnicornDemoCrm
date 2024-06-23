using Core.GenericRepository;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Domain.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    public Task<List<Ticket>> GetAllTicketsAsync(CancellationToken cancellationToken);
    public Task<Ticket> GetTicketWithUserAsync(Guid ticketId, CancellationToken cancellationToken);
}