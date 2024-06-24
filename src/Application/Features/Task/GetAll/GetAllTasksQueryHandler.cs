using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Task.GetAll
{
    public class GetAllTasksQueryHandler(
        ITaskRepository taskRepository,
        ITaskAttendeeRepository taskAttendeeRepository,
        IHttpContextAccessor httpContextAccessor
        ) : IRequestHandler<GetAllTasksQueryRequest, IDataResult<List<GetAllTasksQueryResponse>>>
    {
        public async Task<IDataResult<List<GetAllTasksQueryResponse>>> Handle(GetAllTasksQueryRequest request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                return new ErrorDataResult<List<GetAllTasksQueryResponse>>("Access Token bulunamadı.");

            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId is null)
                return new ErrorDataResult<List<GetAllTasksQueryResponse>>("Kullanıcı girişi yapın.");

            var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager"))
                return new ErrorDataResult<List<GetAllTasksQueryResponse>>("Yetkisiz erişim.");

            var tasks = await taskRepository.GetAllAsync();
            tasks = tasks.Where(x => x!.IsDeleted == false).ToList();
            if (!tasks.Any())
                return new ErrorDataResult<List<GetAllTasksQueryResponse>>("Görev bulunamadı.");

            var taskResponse = new List<GetAllTasksQueryResponse>();
            foreach (var task in tasks)
            {
                var attendees = await taskAttendeeRepository.GetAllAsync();
                var userIds = attendees
                    .Where(a => a!.TaskId == task!.Id)
                    .Select(a => a!.UserId)
                    .ToList();

                var response = new GetAllTasksQueryResponse
                {
                    Id = task!.Id,
                    Title = task!.Title,
                    Description = task.Description,
                    Deadline = task!.Deadline,
                    UserIds = userIds
                };
                taskResponse.Add(response);
            }

            return new SuccessDataResult<List<GetAllTasksQueryResponse>>(taskResponse);

            throw new NotImplementedException();
        }
    }
}