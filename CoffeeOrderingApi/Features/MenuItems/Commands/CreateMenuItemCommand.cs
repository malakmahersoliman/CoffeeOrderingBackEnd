using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using System.Data.Common;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands
{
    public class CreateMenuItemCommand : IRequest<MenuItemResponseDto>
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
