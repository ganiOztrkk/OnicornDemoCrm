using Core.ResultPattern;
using MediatR;

namespace Application.Features.Announcement.GetAll;

public class GetAllAnnouncementsQueryRequest : IRequest<IDataResult<List<GetAllAnnouncementsQueryResponse>>>
{ }