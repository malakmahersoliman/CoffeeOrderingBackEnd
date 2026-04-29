namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class CoffeeOrderItemResponseDto
    {
        public int MenuItemId { get; set; }

        public string MenuItemName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }
    }
}