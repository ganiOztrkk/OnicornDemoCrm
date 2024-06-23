using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class TicketDetailRepository(ApplicationDbContext context): GenericRepository<TicketDetail, ApplicationDbContext>(context), ITicketDetailRepository
{
    public async Task<List<TicketDetail>> GetTicketDetails(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticketDetails = await context.TicketDetails
            .Where(x => x.TicketId == ticketId)
            .OrderBy(x => x.CreateDate)
            .ToListAsync(cancellationToken);
        return ticketDetails;
    }
}