using Core.ResultPattern;
using MediatR;

namespace Application.Features.Task.GetAll
{
    public class GetAllTasksQueryRequest : IRequest<IDataResult<List<GetAllTasksQueryResponse>>>
    {
    }
}
