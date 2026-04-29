using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApi.DTOs
{
    public class CreateMenuIteamDto
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Category { get; set; } = "";

        [Range(0, 300)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
