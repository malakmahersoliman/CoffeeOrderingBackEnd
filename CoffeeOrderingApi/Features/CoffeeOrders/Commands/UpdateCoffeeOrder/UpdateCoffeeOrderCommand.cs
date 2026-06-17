using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.UpdateCoffeeOrder
{
    public class UpdateCoffeeOrderCommand : IRequest<CoffeeOrderResponseDto?>
    {
        public int Id { get; set; }
        public string Status { get; set; } = "";
        public bool IsTakeAway { get; set; }
    }
}
