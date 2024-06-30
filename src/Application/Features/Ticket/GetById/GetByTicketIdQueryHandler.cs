using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Ticket.GetById;

public class GetByTicketIdQueryHandler(
    ITicketRepository repository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetByTicketIdQueryRequest, IDataResult<Domain.Entities.Ticket>>
{
    public async Task<IDataResult<Domain.Entities.Ticket>> Handle(GetByTicketIdQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<Domain.Entities.Ticket>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<Domain.Entities.Ticket>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("coordinator") && !roles.Contains("manager") && !roles.Contains("support")) 
            return new ErrorDataResult<Domain.Entities.Ticket>("Yetkisiz erişim.");
        
        var getByTicketIdValidator = new GetByTicketIdQueryRequestValidator();
        var validationResult = await getByTicketIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<Domain.Entities.Ticket>(errors);
        }

        var ticket = await repository.GetTicketWithUserAsync(request.TicketId, cancellationToken);
        if (ticket is null)
            return new ErrorDataResult<Domain.Entities.Ticket>("Mesaj bulunamadı.");
        
        return new SuccessDataResult<Domain.Entities.Ticket>(ticket);
    }
}