using Application.Features.User.Create;
using Application.Features.User.Delete;
using Application.Features.User.GetAll;
using Application.Features.User.GetById;
using Application.Features.User.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class UsersController(
    IMediator mediator
    ) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById(GetByUserIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var request = new GetAllUsersQueryRequest();
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}