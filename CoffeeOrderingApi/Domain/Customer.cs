namespace CoffeeOrderingApiWithCQRSandMediatR.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public List<CoffeeOrder> CoffeeOrders   { get; set; } = new();
    }
}
