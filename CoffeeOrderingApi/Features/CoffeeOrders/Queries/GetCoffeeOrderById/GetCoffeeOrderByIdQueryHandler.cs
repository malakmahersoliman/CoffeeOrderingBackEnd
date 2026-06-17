using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Mapping;
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

            if (order == null)
            {
                return null;
            }

            return CoffeeOrderMapper.MapToResponseDto(order);
        }
    }
}
