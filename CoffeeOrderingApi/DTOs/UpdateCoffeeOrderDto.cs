using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApi.DTOs
{
    public class UpdateCoffeeOrderDto
    {
        [Required]
        public string Status { get; set; } = "";

        public bool IstakeAway { get; set; }
    }
}
