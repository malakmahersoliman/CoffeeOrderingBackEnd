using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemCommand : IRequest<MenuItemResponseDto>
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
