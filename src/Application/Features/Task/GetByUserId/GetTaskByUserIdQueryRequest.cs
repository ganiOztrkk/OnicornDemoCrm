using Core.ResultPattern;
using MediatR;

namespace Application.Features.Task.GetByUserId
{
    public class GetTaskByUserIdQueryRequest : IRequest<IDataResult<List<GetTaskByUserIdQueryResponse>>>
    {
        public Guid UserId { get; set; }
    }
}
