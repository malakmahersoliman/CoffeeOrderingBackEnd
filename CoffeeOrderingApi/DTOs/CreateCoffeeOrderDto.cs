using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CreateCoffeeOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        public bool IsTakeAway { get; set; }

        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}