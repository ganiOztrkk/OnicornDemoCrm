using Application.Features.Sale.Create;
using Application.Features.Sale.Delete;
using Application.Features.Sale.GetAll;
using Application.Features.Sale.GetById;
using Application.Features.Sale.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class SalesController(
    IMediator mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var request = new GetAllSaleQueryRequest();
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById(GetBySaleIdQueryRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleCommandRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteSaleCommandRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateSaleCommandRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}