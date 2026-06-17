using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CreateMenuItemDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; } = "";

        [Range(0.01, 1000.0, ErrorMessage = "Price must be between 0.01 and 1000.00")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
