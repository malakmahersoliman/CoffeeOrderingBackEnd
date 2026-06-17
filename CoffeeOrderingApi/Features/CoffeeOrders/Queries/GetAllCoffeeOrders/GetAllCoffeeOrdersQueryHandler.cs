using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetAllCoffeeOrders
{
    public class GetAllCoffeeOrdersQueryHandler : IRequestHandler<GetAllCoffeeOrdersQuery, List<CoffeeOrderResponseDto>>
    {
        private readonly AppDbContext _context;

        public GetAllCoffeeOrdersQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CoffeeOrderResponseDto>> Handle(GetAllCoffeeOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync(cancellationToken);

            return orders.Select(MapToResponseDto).ToList();
        }

        private static CoffeeOrderResponseDto MapToResponseDto(CoffeeOrder order)
        {
            var items = order.OrderItems.Select(oi => new CoffeeOrderItemResponseDto
            {
                MenuItemId = oi.MenuItemId,
                MenuItemName = oi.MenuItem?.Name ?? "Unknown Item",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                SubTotal = oi.UnitPrice * oi.Quantity
            }).ToList();

            return new CoffeeOrderResponseDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer?.FullName ?? "Unknown Customer",
                IsTakeAway = order.IsTakeAway,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalAmount = items.Sum(i => i.SubTotal),
                Items = items
            };
        }
    }
}
