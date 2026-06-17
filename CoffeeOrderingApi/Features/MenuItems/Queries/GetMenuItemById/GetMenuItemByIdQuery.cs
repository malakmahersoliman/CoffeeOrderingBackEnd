using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetMenuItemById
{
    public class GetMenuItemByIdQuery : IRequest<MenuItemResponseDto?>
    {
        public int Id { get; set; }
    }
}
