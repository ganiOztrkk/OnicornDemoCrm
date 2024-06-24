using Core.GenericRepository;
using Domain.Entities;
using Task = Domain.Entities.Task;

namespace Domain.Repositories;

public interface ITaskRepository : IGenericRepository<Task>
{
    Task<List<Task>> GetTasksByUserIdAsync(Guid userId);
}