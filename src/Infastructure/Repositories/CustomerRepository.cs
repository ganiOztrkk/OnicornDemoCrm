using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context)
    : GenericRepository<Customer, ApplicationDbContext>(context), ICustomerRepository
{
    public async Task<Customer?> GetByNamesAsync(string? name, string? companyName)
    {
        return await context.Customers.FirstOrDefaultAsync(c => c.Name == name && c.CompanyName == companyName);
    }

    public async Task<bool> ExistsAsync(Guid customerId)
    {
        return await context.Customers.AnyAsync(c => c.Id == customerId);
    }
}