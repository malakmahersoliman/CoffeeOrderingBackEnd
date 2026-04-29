namespace CoffeeOrderingApiWithCQRSandMediatR.DTOs
{
    public class MenuItemResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Category { get; set; } = "";

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }



    }
}
