using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetCoffeeOrderById
{
    public class GetCoffeeOrderByIdQuery : IRequest<CoffeeOrderResponseDto?>
    {
        public int Id { get; }

        public GetCoffeeOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
