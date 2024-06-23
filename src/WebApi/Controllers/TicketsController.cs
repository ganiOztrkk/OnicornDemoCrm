using Application.Features.Ticket.Create;
using Application.Features.Ticket.CreateDetailContent;
using Application.Features.Ticket.GetAll;
using Application.Features.Ticket.GetById;
using Application.Features.Ticket.GetDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class TicketsController(
    IMediator mediator
    ) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDetailContent(CreateTicketDetailContentCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var request = new GetAllTicketsQueryRequest();
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetDetails(GetTicketDetailQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById(GetByTicketIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}