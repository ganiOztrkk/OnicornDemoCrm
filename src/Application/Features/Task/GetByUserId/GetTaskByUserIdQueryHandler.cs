using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Task.GetByUserId
{
    public class GetTaskByUserIdQueryHandler(
    ITaskRepository repository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetTaskByUserIdQueryRequest, IDataResult<List<GetTaskByUserIdQueryResponse>>>
    {
        public async Task<IDataResult<List<GetTaskByUserIdQueryResponse>>> Handle(GetTaskByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                return new ErrorDataResult<List<GetTaskByUserIdQueryResponse>>("Access Token bulunamadı.");

            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId is null)
                return new ErrorDataResult<List<GetTaskByUserIdQueryResponse>>("Kullanıcı girişi yapın.");

            var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager") && !roles.Contains("salesperson"))
                return new ErrorDataResult<List<GetTaskByUserIdQueryResponse>>("Yetkisiz erişim.");

            var getTaskByUserIdValidator = new GetTaskByUserIdQueryRequestValidator();
            var validationResult = await getTaskByUserIdValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
                return new ErrorDataResult<List<GetTaskByUserIdQueryResponse>>(errors);
            }

            var tasks = await repository.GetTasksByUserIdAsync(request.UserId);
            if (!tasks.Any())
                return new ErrorDataResult<List<GetTaskByUserIdQueryResponse>>("Görev bulunamadı.");

            var taskList = new List<GetTaskByUserIdQueryResponse>();
            foreach (var item in tasks)
            {
                var task = new GetTaskByUserIdQueryResponse
                {
                    Title = item.Title,
                    Description = item.Description,
                    Deadline = item.Deadline
                };
                taskList.Add(task);
            }
            return new SuccessDataResult<List<GetTaskByUserIdQueryResponse>>(taskList);
        }
    }
}
