using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class UpdateCoffeeOrderDto
    {
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "";

        public bool IsTakeAway { get; set; }
    }
}
