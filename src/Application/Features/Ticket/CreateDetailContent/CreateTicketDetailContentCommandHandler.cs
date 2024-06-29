using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Ticket.CreateDetailContent;

public class CreateTicketDetailContentCommandHandler(
    ITicketDetailRepository repository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateTicketDetailContentCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateTicketDetailContentCommandRequest request, CancellationToken cancellationToken)
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

        var createMessageDetailContentValidator = new CreateTicketDetailContentCommandRequestValidator();
        var validationResult = await createMessageDetailContentValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var message = repository.GetAsync(request.TicketId);
        var messageDetail = new TicketDetail
        {
            TicketId = request.TicketId,
            Content = request.Content,
            CreateDate = DateTime.Now,
            AppUserId = request.AppUserId
        };
        await repository.InsertAsync(messageDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Mesaj gönderildi.");
    }
}