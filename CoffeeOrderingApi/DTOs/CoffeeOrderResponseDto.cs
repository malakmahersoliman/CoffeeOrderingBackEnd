namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CoffeeOrderResponseDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = "";

        public bool IsTakeAway { get; set; }

        public string Status { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public decimal TotalAmount { get; set; }

        public List<CoffeeOrderItemResponseDto> Items { get; set; } = new();
    }
}