using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Infastructure.Repositories;

public class TaskRepository(ApplicationDbContext context) : GenericRepository<Task, ApplicationDbContext>(context), ITaskRepository
{
    public async Task<List<Task>> GetTasksByUserIdAsync(Guid userId)
    {
        return (await context.TaskAttendees
            .Where(ma => ma.UserId == userId)
            .Select(ma => ma.Task)
            .ToListAsync())!;
    }
}