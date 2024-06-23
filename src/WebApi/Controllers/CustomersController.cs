using Application.Features.Customer.Create;
using Application.Features.Customer.Delete;
using Application.Features.Customer.GetAll;
using Application.Features.Customer.GetById;
using Application.Features.Customer.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class CustomersController(
    IMediator mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var request = new GetAllCustomersQueryRequest();
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById(GetByCustomerIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}