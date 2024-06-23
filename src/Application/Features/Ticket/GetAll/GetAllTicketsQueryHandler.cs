using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Ticket.GetAll;

public class GetAllTicketsQueryHandler(
    ITicketRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetAllTicketsQueryRequest, IDataResult<List<GetAllTicketsQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllTicketsQueryResponse>>> Handle(GetAllTicketsQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetAllTicketsQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetAllTicketsQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorDataResult<List<GetAllTicketsQueryResponse>>("Yetkisiz erişim.");

        
        if (roles.Contains("admin") || roles.Contains("support"))
        {
            var allTickets = await repository.GetAllTicketsAsync(cancellationToken);
            
            var response = mapper.Map<List<GetAllTicketsQueryResponse>>(allTickets);
            return new SuccessDataResult<List<GetAllTicketsQueryResponse>>(response);
        }
        else
        {
            var allTickets = await repository.GetAllAsync();
            var filteredTickets = allTickets
                .Where(x => x!.AppUserId == Guid.Parse(userId))
                .Select(x => new GetAllTicketsQueryResponse(x!.Id, x.Subject, x.CreateDate))
                .ToList();
            
            var response = mapper.Map<List<GetAllTicketsQueryResponse>>(filteredTickets);
            return new SuccessDataResult<List<GetAllTicketsQueryResponse>>(response);
        }
    }
}