using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Mapping;
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

            return orders.Select(CoffeeOrderMapper.MapToResponseDto).ToList();
        }
    }
}
