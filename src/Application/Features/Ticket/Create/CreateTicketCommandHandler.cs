using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Ticket.Create;


public class CreateTicketCommandHandler(
    ITicketRepository repository,
    ITicketDetailRepository ticketDetailRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<CreateTicketCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateTicketCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var createMessageValidator = new CreateTicketCommandRequestValidator();
        var validationResult = await createMessageValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var ticket = new Domain.Entities.Ticket
        {
            AppUserId = Guid.Parse(userId),
            Subject = request.Subject,
            CreateDate = DateTime.Now
        };
        await repository.InsertAsync(ticket);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var ticketDetail = new TicketDetail
        {
            TicketId = ticket.Id,
            AppUserId = Guid.Parse(userId),
            Content = request.Summary,
            CreateDate = ticket.CreateDate
        };
        await ticketDetailRepository.InsertAsync(ticketDetail);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Mesaj gönderildi.");
    }
}