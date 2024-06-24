using Core.ResultPattern;
using MediatR;

namespace Application.Features.Task.Delete
{
    public class DeleteTaskCommandRequest : IRequest<IResult>
    {
        public Guid TaskId { get; set; }
    }
}
