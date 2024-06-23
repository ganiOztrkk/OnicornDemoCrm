using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class TicketRepository(ApplicationDbContext context) : GenericRepository<Ticket, ApplicationDbContext>(context), ITicketRepository
{
    public async Task<List<Ticket>> GetAllTicketsAsync(CancellationToken cancellationToken)
    {
        var allTickets = await context.Tickets
            .Select(x => new Ticket
            {
                Id = x.Id,
                Subject = x.Subject,
                CreateDate = x.CreateDate
            }).ToListAsync(cancellationToken);
        
        return allTickets;
    }

    public async Task<Ticket> GetTicketWithUserAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await context.Tickets
            .Include(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.Id == ticketId, cancellationToken);
        return ticket!;
    }
}