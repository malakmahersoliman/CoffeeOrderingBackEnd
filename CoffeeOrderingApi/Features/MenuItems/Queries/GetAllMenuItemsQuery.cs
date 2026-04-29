using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries
{
    public class GetAllMenuItemsQuery : IRequest<List<MenuItemResponseDto>>
    {
    }
}
