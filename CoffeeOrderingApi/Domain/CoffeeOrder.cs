namespace CoffeeOrderingApiWithCQRSandMediatR.Domain
{
    public class CoffeeOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;


        public bool IsTakeAway { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
