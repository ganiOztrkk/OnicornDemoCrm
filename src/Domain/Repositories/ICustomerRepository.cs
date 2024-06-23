using Core.GenericRepository;
using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetByNamesAsync(string? name, string? companyName);
    Task<bool> ExistsAsync(Guid customerId);
}