using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Mapping
{
    public static class CoffeeOrderMapper
    {
        public static CoffeeOrderResponseDto MapToResponseDto(CoffeeOrder order)
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
