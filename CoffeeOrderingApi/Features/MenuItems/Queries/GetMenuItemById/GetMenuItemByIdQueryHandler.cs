using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetMenuItemById
{
    public class GetMenuItemByIdQueryHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItemResponseDto?>
    {

        private readonly AppDbContext _context;

        public GetMenuItemByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<MenuItemResponseDto?> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var menuItem = await _context.MenuItems.FindAsync(request.Id);
            if (menuItem == null)
            {
                return null;
            }
            return new MenuItemResponseDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Category = menuItem.Category,
                Price = menuItem.Price,
                IsAvailable = menuItem.IsAvailable
            };

        }
    }
}
