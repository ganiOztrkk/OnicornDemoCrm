namespace Application.Features.Ticket.GetAll;

public sealed record GetAllTicketsQueryResponse(
    Guid Id,
    string Subject,
    DateTime CreateDate
);