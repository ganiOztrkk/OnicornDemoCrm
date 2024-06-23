using Core.ResultPattern;
using MediatR;

namespace Application.Features.User.Update;

public class UpdateUserCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}