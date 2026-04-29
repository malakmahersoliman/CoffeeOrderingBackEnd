using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.UpdateMenuItems
{
    public class UpdateMenuItemsCommand : IRequest<MenuItemResponseDto?>
    {
       public int Id { get; set; }
        public string Name { get; set; } = "";

        public string Category { get; set; } = "";

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }


    }
}
