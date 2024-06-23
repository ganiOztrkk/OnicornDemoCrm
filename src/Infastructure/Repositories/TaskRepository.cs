using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Task = Domain.Entities.Task;

namespace Infastructure.Repositories;

public class TaskRepository(ApplicationDbContext context) : GenericRepository<Task, ApplicationDbContext>(context), ITaskRepository
{
    
}