using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApi.DTOs
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } ="";

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = "";
    }
}
