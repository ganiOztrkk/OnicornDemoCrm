using Core.GenericRepository;
using Task = Domain.Entities.Task;

namespace Domain.Repositories;

public interface ITaskRepository : IGenericRepository<Task>
{
    
}