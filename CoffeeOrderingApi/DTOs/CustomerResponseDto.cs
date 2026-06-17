namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}
