using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetAllMenuItems
{
    public class GetAllMenuItemsQuery : IRequest<List<MenuItemResponseDto>>
    {
    }
}
