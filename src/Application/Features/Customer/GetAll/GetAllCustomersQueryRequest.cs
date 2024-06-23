using Core.ResultPattern;
using MediatR;

namespace Application.Features.Customer.GetAll;

public sealed record GetAllCustomersQueryRequest() : IRequest<IDataResult<List<GetAllCustomersQueryResponse>>>;