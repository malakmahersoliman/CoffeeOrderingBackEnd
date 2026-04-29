using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.CreateMenuItems
{
    public class CreateMenuItemsCommandHandler
        : IRequestHandler<CreateMenuItemsCommand, MenuItemResponseDto>
    {
        private readonly AppDbContext _context;
        public CreateMenuItemsCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<MenuItemResponseDto> Handle(CreateMenuItemsCommand request, CancellationToken cancellationToken)
        {
            var newMenuItem = new MenuItem
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                IsAvailable = request.IsAvailable
            };
            _context.MenuItems.Add(newMenuItem);
            await _context.SaveChangesAsync(cancellationToken);
            return new MenuItemResponseDto
            {
                Id = newMenuItem.Id,
                Name = newMenuItem.Name,
                Category = newMenuItem.Category,
                Price = newMenuItem.Price,
                IsAvailable = newMenuItem.IsAvailable
            };
        }
    }
}
