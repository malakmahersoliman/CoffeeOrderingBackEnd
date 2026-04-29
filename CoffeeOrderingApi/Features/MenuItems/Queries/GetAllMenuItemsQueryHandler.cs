using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries
{
    public class GetAllMenuItemsQueryHandler
        : IRequestHandler<GetAllMenuItemsQuery, List<MenuItemResponseDto>>
    {

        private readonly AppDbContext _context;
        public GetAllMenuItemsQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<MenuItemResponseDto>>
            Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
           return await _context.MenuItems
                .Select(mi => new MenuItemResponseDto
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    Category = mi.Category,
                    Price = mi.Price,
                    IsAvailable = mi.IsAvailable
                })
                .ToListAsync(cancellationToken);
        }
    }
}
