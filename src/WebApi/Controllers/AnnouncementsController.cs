using Application.Features.Announcement.Create;
using Application.Features.Announcement.Delete;
using Application.Features.Announcement.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AnnouncementsController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnouncementCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var request = new GetAllAnnouncementsQueryRequest();
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAnnouncementCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}