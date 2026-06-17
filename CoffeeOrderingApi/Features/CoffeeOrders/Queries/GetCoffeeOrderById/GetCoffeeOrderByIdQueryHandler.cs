using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetCoffeeOrderById
{
    public class GetCoffeeOrderByIdQueryHandler : IRequestHandler<GetCoffeeOrderByIdQuery, CoffeeOrderResponseDto?>
    {
        private readonly AppDbContext _context;

        public GetCoffeeOrderByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoffeeOrderResponseDto?> Handle(GetCoffeeOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null) return null;

            return MapToResponseDto(order);
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
