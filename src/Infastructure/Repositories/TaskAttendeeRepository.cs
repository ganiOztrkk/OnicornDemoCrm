using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Repositories
{
    public class TaskAttendeeRepository(ApplicationDbContext context) : GenericRepository<TaskAttendee, ApplicationDbContext>(context), ITaskAttendeeRepository
    {
        public async Task<List<TaskAttendee>> GetByTaskIdAsync(Guid taskId)
        {
            return await context.TaskAttendees
                .Where(ma => ma.TaskId == taskId)
                .ToListAsync();
        }
    }
}
