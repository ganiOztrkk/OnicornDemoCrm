using Core.ResultPattern;
using MediatR;

namespace Application.Features.User.Delete;

public class DeleteUserCommandRequest : IRequest<IResult>
{
    public Guid UserId { get; set; }
}