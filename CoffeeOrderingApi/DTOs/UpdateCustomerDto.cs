using System.ComponentModel.DataAnnotations;

namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class UpdateCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } ="";

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = "";
    }
}
