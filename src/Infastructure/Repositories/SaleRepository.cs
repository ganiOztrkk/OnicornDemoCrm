using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;

namespace Infastructure.Repositories;

public class SaleRepository(ApplicationDbContext context) : GenericRepository<Sale, ApplicationDbContext>(context), ISaleRepository
{
    
}