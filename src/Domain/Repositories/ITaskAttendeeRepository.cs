using Core.GenericRepository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITaskAttendeeRepository : IGenericRepository<TaskAttendee>
    {
        Task<List<TaskAttendee>> GetByTaskIdAsync(Guid taskId);
    }
}
