using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.UpdateMenuItems
{
    public class UpdateMenuItemsCommandHandler : IRequestHandler<UpdateMenuItemsCommand, MenuItemResponseDto?>
    {
        private readonly AppDbContext _context;
        public UpdateMenuItemsCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<MenuItemResponseDto?> Handle(UpdateMenuItemsCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _context.MenuItems.FindAsync(request.Id);
            if (menuItem == null) {
                return null;
            }
            menuItem.Name = request.Name;
            menuItem.Category = request.Category;
            menuItem.Price = request.Price;
            menuItem.IsAvailable = request.IsAvailable;
            await _context.SaveChangesAsync(cancellationToken);
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
