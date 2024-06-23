using FluentValidation;

namespace Application.Features.Ticket.GetById;

public class GetByTicketIdQueryRequestValidator : AbstractValidator<GetByTicketIdQueryRequest>
{
    public GetByTicketIdQueryRequestValidator()
    {
        
    }
}