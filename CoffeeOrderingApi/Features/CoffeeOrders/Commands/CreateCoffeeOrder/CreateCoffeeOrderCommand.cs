using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.CreateCoffeeOrder
{
    public class CreateCoffeeOrderCommand : IRequest<CoffeeOrderResponseDto?>
    {
        [Required]
        public int CustomerId { get; set; }

        public bool IsTakeAway { get; set; }

        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
