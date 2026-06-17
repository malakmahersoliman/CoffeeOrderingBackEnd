using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, MenuItemResponseDto?>
    {
        private readonly AppDbContext _context;

        public UpdateMenuItemCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItemResponseDto?> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _context.MenuItems.FindAsync(new object[] { request.Id }, cancellationToken);
            if (menuItem == null)
            {
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
