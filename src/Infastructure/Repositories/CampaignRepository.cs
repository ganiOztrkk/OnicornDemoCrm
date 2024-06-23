using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;

namespace Infastructure.Repositories;

public class CampaignRepository(ApplicationDbContext context) : GenericRepository<Campaign, ApplicationDbContext>(context), ICampaignRepository
{
    
}