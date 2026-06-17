using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetCoffeeOrderById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.CreateCoffeeOrder
{
    public class CreateCoffeeOrderCommandHandler : IRequestHandler<CreateCoffeeOrderCommand, CoffeeOrderResponseDto?>
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public CreateCoffeeOrderCommandHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CoffeeOrderResponseDto?> Handle(CreateCoffeeOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
            {
                throw new ArgumentException("An order must contain at least one item.", nameof(request.Items));
            }

            foreach (var item in request.Items)
            {
                if (item.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity for item must be greater than zero.", nameof(item.Quantity));
                }
            }

            // Validate Customer exists
            var customer = await _context.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            // Retrieve MenuItems
            var menuItemIds = request.Items.Select(i => i.MenuItemId).Distinct().ToList();
            var menuItems = await _context.MenuItems
                .Where(m => menuItemIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, cancellationToken);

            // Validate all requested menu items exist and are available
            foreach (var requestedItem in request.Items)
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
                CustomerId = request.CustomerId,
                IsTakeAway = request.IsTakeAway,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            foreach (var requestedItem in request.Items)
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
            await _context.SaveChangesAsync(cancellationToken);

            // Fetch and return the fully populated order using the query
            return await _mediator.Send(new GetCoffeeOrderByIdQuery(order.Id), cancellationToken);
        }
    }
}
