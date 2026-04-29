using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public int MenuItemId { get; set; }

        [Range(1, 20)]
        public int Quantity { get; set; }
    }
}