using Application.Features.Meeting.GetByUserId;
using Application.Features.Task.Create;
using Application.Features.Task.Delete;
using Application.Features.Task.GetAll;
using Application.Features.Task.GetByUserId;
using Application.Features.Task.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class TasksController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var request = new GetAllTasksQueryRequest();
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetByUserId(GetTaskByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
