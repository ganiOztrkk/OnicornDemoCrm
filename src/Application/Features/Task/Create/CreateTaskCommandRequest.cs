using Core.ResultPattern;
using MediatR;

namespace Application.Features.Task.Create
{
    public class CreateTaskCommandRequest : IRequest<IResult>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}