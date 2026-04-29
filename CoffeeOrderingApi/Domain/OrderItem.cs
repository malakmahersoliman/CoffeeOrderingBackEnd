namespace CoffeeOrderingApiWithCQRSandMediatR.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int CoffeeOrderId { get; set; }

        public CoffeeOrder CoffeeOrder { get; set; } = null!;


        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;


        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


    }
}
