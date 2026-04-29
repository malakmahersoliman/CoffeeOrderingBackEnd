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

        public async Task<List<CoffeeOrderResponseDto>> GetAll()
        {
            return await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Select(o => new CoffeeOrderResponseDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.FullName,
                    IsTakeAway = o.IsTakeAway,
                    Status = o.Status,
                    CreatedAt = o.CreatedAt,

                    Items = o.OrderItems.Select(oi => new CoffeeOrderItemResponseDto
                    {
                        MenuItemId = oi.MenuItemId,
                        MenuItemName = oi.MenuItem.Name,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        SubTotal = oi.Quantity * oi.UnitPrice
                    }).ToList(),

                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .ToListAsync();
        }

        public async Task<CoffeeOrderResponseDto?> GetById(int id)
        {
            return await _context.CoffeeOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Id == id)
                .Select(o => new CoffeeOrderResponseDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.FullName,
                    IsTakeAway = o.IsTakeAway,
                    Status = o.Status,
                    CreatedAt = o.CreatedAt,

                    Items = o.OrderItems.Select(oi => new CoffeeOrderItemResponseDto
                    {
                        MenuItemId = oi.MenuItemId,
                        MenuItemName = oi.MenuItem.Name,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        SubTotal = oi.Quantity * oi.UnitPrice
                    }).ToList(),

                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CoffeeOrderResponseDto?> Create(CreateCoffeeOrderDto dto)
        {
            var customerExists = await _context.Customers
                .AnyAsync(c => c.Id == dto.CustomerId);

            if (!customerExists)
            {
                return null;
            }

            var menuItemIds = dto.Items.Select(i => i.MenuItemId).ToList();

            var menuItems = await _context.MenuItems
                .Where(m => menuItemIds.Contains(m.Id) && m.IsAvailable)
                .ToListAsync();

            if (menuItems.Count != menuItemIds.Distinct().Count())
            {
                return null;
            }

            var order = new CoffeeOrder
            {
                CustomerId = dto.CustomerId,
                IsTakeAway = dto.IsTakeAway,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                OrderItems = dto.Items.Select(itemDto =>
                {
                    var menuItem = menuItems.First(m => m.Id == itemDto.MenuItemId);

                    return new OrderItem
                    {
                        MenuItemId = itemDto.MenuItemId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = menuItem.Price
                    };
                }).ToList()
            };

            _context.CoffeeOrders.Add(order);
            await _context.SaveChangesAsync();

            return await GetById(order.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _context.CoffeeOrders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            _context.CoffeeOrders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}