using Core.ResultPattern;
using MediatR;

namespace Application.Features.Sale.GetAll;

public sealed record GetAllSaleQueryRequest() : IRequest<IDataResult<List<GetAllSaleQueryResponse>>>;