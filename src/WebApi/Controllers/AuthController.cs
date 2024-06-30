using Application.Features.Auth.Login;
using Domain.Entities;
using Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Abstractions;

namespace WebApi.Controllers;

[AllowAnonymous]
public class AuthController(
    IMediator mediator,
    ApplicationDbContext context,
    RoleManager<AppRole> roleManager) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    public IActionResult Users()
    {
        var user = context.Users.ToList();
        return Ok(user);
    }
    
    [HttpGet]
    public IActionResult Sales()
    {
        var sales = context.Sales.ToList();
        return Ok(sales);
    }
    
    [HttpGet]
    public IActionResult Messages()
    {
        var messages = context.Tickets.ToList();
        var messageDetails = context.TicketDetails.ToList();
        return Ok(new {message= messages, details= messageDetails});
    }
    
    [HttpGet]
    public IActionResult Roles()
    {
        var roles = context.AppUserRoles.ToList();
        return Ok(roles);
    }
    [HttpGet]
    public IActionResult RoleNames()
    {
        var salespersonRole = roleManager.Roles.ToList();
        return Ok(salespersonRole);
    }
}