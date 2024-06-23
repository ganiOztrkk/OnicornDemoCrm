using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Ticket.GetDetail;

public class GetTicketDetailQueryHandler(
    ITicketDetailRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetTicketDetailQueryRequest, IDataResult<List<TicketDetail>>>
{
    public async Task<IDataResult<List<TicketDetail>>> Handle(GetTicketDetailQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<TicketDetail>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<TicketDetail>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorDataResult<List<TicketDetail>>("Yetkisiz erişim.");
        
        var getMessageDetailValidator = new GetTicketDetailQueryRequestValidator();
        var validationResult = await getMessageDetailValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<List<TicketDetail>>(errors);
        }

        var messageDetails = await repository.GetTicketDetails(request.TicketId, cancellationToken);
        if (messageDetails.IsNullOrEmpty())
            return new ErrorDataResult<List<TicketDetail>>("Mesaj bulunamadı.");
        
        return new SuccessDataResult<List<TicketDetail>>(messageDetails);
    }
}