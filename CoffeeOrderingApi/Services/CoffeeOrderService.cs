using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public class CoffeeOrderService : ICoffeeOrderService
    {
        private readonly AppDbContext _context;

        public CoffeeOrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoffeeOrderResponseDto>> GetAllAsync()
        {
            var orders = await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            return orders.Select(MapToResponseDto);
        }

        public async Task<CoffeeOrderResponseDto?> GetByIdAsync(int id)
        {
            var order = await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return MapToResponseDto(order);
        }

        public async Task<CoffeeOrderResponseDto?> CreateAsync(CreateCoffeeOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
            {
                throw new ArgumentException("An order must contain at least one item.", nameof(dto.Items));
            }

            foreach (var item in dto.Items)
            {
                if (item.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity for item must be greater than zero.", nameof(item.Quantity));
                }
            }

            // Validate Customer exists
            var customer = await _context.Customers.FindAsync(dto.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found.");
            }

            // Retrieve MenuItems
            var menuItemIds = dto.Items.Select(i => i.MenuItemId).Distinct().ToList();
            var menuItems = await _context.MenuItems
                .Where(m => menuItemIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id);

            // Validate all requested menu items exist and are available
            foreach (var requestedItem in dto.Items)
            {
                if (!menuItems.TryGetValue(requestedItem.MenuItemId, out var menuItem))
                {
                    throw new KeyNotFoundException($"MenuItem with ID {requestedItem.MenuItemId} not found.");
                }

                if (!menuItem.IsAvailable)
                {
                    throw new InvalidOperationException($"MenuItem '{menuItem.Name}' (ID {menuItem.Id}) is currently unavailable.");
                }
            }

            // Create Order
            var order = new CoffeeOrder
            {
                CustomerId = dto.CustomerId,
                IsTakeAway = dto.IsTakeAway,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            foreach (var requestedItem in dto.Items)
            {
                var menuItem = menuItems[requestedItem.MenuItemId];
                var orderItem = new OrderItem
                {
                    MenuItemId = requestedItem.MenuItemId,
                    Quantity = requestedItem.Quantity,
                    UnitPrice = menuItem.Price
                };
                order.OrderItems.Add(orderItem);
            }

            _context.CoffeeOrders.Add(order);
            await _context.SaveChangesAsync();

            // Refresh EF context to get fully loaded associations
            return await GetByIdAsync(order.Id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.CoffeeOrders.FindAsync(id);
            if (order == null) return false;

            _context.CoffeeOrders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
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
