using Application.Features.Task.Create;
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
    }
}
