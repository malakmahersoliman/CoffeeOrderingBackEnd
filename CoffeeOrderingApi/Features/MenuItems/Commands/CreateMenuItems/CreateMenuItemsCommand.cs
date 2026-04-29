using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using System.Data.Common;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.CreateMenuItems
{
    public class CreateMenuItemsCommand : IRequest<MenuItemResponseDto>
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
