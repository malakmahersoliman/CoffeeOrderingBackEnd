using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApi.DTOs
{
    public class UpdateMenuItemDto
    {
   
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Category { get; set; } = "";

        [Range(1,300)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
