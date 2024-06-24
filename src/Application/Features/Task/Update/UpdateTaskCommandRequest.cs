using Core.ResultPattern;
using MediatR;

namespace Application.Features.Task.Update
{
    public class UpdateTaskCommandRequest : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
