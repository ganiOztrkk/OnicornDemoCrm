using Core.ResultPattern;
using MediatR;

namespace Application.Features.Customer.Update;

public sealed record UpdateCustomerCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? CompanyName { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? ContactPerson { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public string? ShoppingArea { get; set; }
}