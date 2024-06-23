using Core.ResultPattern;
using MediatR;

namespace Application.Features.Meeting.GetAll;

public class GetAllMeetingsQueryRequest : IRequest<IDataResult<List<GetAllMeetingsQueryResponse>>>
{ }