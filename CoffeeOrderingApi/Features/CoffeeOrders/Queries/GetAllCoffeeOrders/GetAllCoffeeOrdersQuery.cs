using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetAllCoffeeOrders
{
    public class GetAllCoffeeOrdersQuery : IRequest<List<CoffeeOrderResponseDto>>
    {
    }
}
