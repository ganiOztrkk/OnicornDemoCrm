using FluentValidation;

namespace Application.Features.Ticket.GetDetail;

public class GetTicketDetailQueryRequestValidator : AbstractValidator<GetTicketDetailQueryRequest>
{
    public GetTicketDetailQueryRequestValidator()
    {
        RuleFor(x => x.TicketId)
            .NotEmpty().WithMessage("ID boş olamaz.")
            .NotNull().WithMessage("ID boş olamaz.");
    }
}