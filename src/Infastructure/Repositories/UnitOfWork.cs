using Core.UnitOfWork;
using Infastructure.Context;

namespace Infastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}